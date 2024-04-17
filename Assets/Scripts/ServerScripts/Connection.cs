using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.WebRTC;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;

public class Connection : MonoBehaviour
{
    // Seperator used for sending Socket.IO signaling messages
    private const string sep = "\n;\n";

    // The current room ID. 
    // Used to connect to this computer from the phone and creating the QR code.
    private static string _RoomID = Guid.NewGuid().ToString();
    public static string RoomID
    {
        get
        {
            return _RoomID;
        }
    }

    // Web server connection
    private SocketIOUnity _socket;

    // Phone connection
    private RTCPeerConnection _remotePeer;
    private static RTCDataChannel _channel;

    public static void MessagePhone(string message)
    {
        lastMessageSent = message;
        nonACKed.Add(message);
    }

    // Listener Callbacks
    private static Dictionary<string, Action<string>> _listeners = new Dictionary<string, Action<string>>();

    public static void SetListener(string name, Action<string> callback)
    {
        _listeners[name] = callback;
    }

    public static void RemoveListener(string name)
    {
        _listeners.Remove(name);
    }

    // ACK list to guarentee messages
    private static string? lastMessageSent = null;
    private static List<string> nonACKed = new List<string>();
    private static List<string> receivedMessages = new List<string>();
    private float timeSinceLastSend = 0f;


    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        Log(RoomID);

        var uri = new Uri("https://echoes-through-the-screen-8aqb2.ondigitalocean.app/");
        _socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Query = new Dictionary<string, string>
            {
                {"token", "UNITY" }
            },
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });
        _socket.JsonSerializer = new NewtonsoftJsonSerializer();

        // Simple connection/disconnection events
        _socket.OnConnected += (sender, e) =>
        {
            Log("Socket.IO connected to Web Server");
            _socket.Emit("create room", RoomID);
        };
        _socket.OnDisconnected += (sender, e) =>
        {
            Log("Disconnect - " + e);
        };
        _socket.OnReconnectAttempt += (sender, e) =>
        {
            Log($"Reconnecting, {DateTime.Now}, Attempt = {e}");
        };

        // Handle Phone WebRTC connection events
        _socket.OnUnityThread("ice-candidate", (data) =>
        {
            Log("Recieving ICE Candidate");

            // values[0] = <candidate>,
            // values[1] = <spdMid>,
            // values[2] = <sdpMLineIndex>
            var incoming = data.GetValue<string>();
            string[] values = incoming.Split(sep);


            var candidateInit = new RTCIceCandidateInit();
            candidateInit.candidate = values[0];
            candidateInit.sdpMid = values[1];
            try
            {
                candidateInit.sdpMLineIndex = Int32.Parse(values[2]);
            }
            catch (Exception)
            {
                candidateInit.sdpMLineIndex = null;
            }

            RTCIceCandidate iceCandidate = new RTCIceCandidate(candidateInit);
            _remotePeer.AddIceCandidate(iceCandidate);
        });

        _socket.OnUnityThread("offer", (data) =>
        {
            Log("Attempting Phone Connection");
            StartCoroutine("handleOffer", data);
        });

        Log("Connecting Socket...");
        _socket.Connect();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSend += Time.deltaTime;

        if (timeSinceLastSend > 1.0f)
        {
            timeSinceLastSend = 0.0f;
            if ((_channel != null) && (_channel.ReadyState == RTCDataChannelState.Open))
            {
                nonACKed.ForEach(delegate (string message)
                {
                    _channel.Send(message);
                });
            }
        }

        if (_channel != null)
        {
            //Log($"Channel State: {_channel.ReadyState}");
            //MessagePhone("Hello");
        }
    }

    private IEnumerator handleOffer(SocketIOResponse data)
    {
        // values[0] = <target>,
        // values[1] = <caller>,
        // values[2] = <sdp>
        var incoming = data.GetValue<string>();
        string[] values = incoming.Split(sep);

        _remotePeer = createPeer(values[1]);

        var givenDesc = new RTCSessionDescription();
        givenDesc.sdp = values[2];
        var remoteDesc = _remotePeer.SetRemoteDescription(ref givenDesc);
        yield return remoteDesc;

        var answer = _remotePeer.CreateAnswer();
        yield return answer;

        RTCSessionDescription answerDesc = answer.Desc;
        var localDesc = _remotePeer.SetLocalDescription(ref answerDesc);
        yield return localDesc;

        /* String in format:
        <target>
        <caller>
        <type>
        <spd>
        */
        string message = $"{values[1]}{sep}{_socket.Id}{sep}{_remotePeer.LocalDescription.type}{sep}{_remotePeer.LocalDescription.sdp}";
        _socket.Emit("answer", message);

        Log("Sending Answer");
    }

    private RTCPeerConnection createPeer(string partnerID)
    {
        var iceServer = new RTCIceServer();
        iceServer.urls = new[] { "stun:stun.stunprotocol.org" };
        var config = new RTCConfiguration();
        config.iceServers = new[] { iceServer };
        var peer = new RTCPeerConnection(ref config);

        peer.OnIceCandidate = (e) => { handleICECandidateEvent(e, partnerID); };
        peer.OnDataChannel = handleDataChannelEvent;

        return peer;
    }

    private void handleICECandidateEvent(RTCIceCandidate e, string partnerID)
    {
        Log("Handling ICE Candidate");
        if ((e.Candidate != null) && (e.Candidate != ""))
        {
            /* String in format:
            <target>
            <candidate>
            */
            string message = $"{partnerID}{sep}{e.Candidate}";
            _socket.Emit("ice-candidate", message);
            Log("Sending ICE Candidate");
        }
    }

    private void handleDataChannelEvent(RTCDataChannel e)
    {
        Log("Data Channel Being Created");
        _channel = e;
        _channel.OnMessage = handleChannelMessage;
        _channel.OnOpen = handleChannelOpen;
        _channel.OnClose = handleChannelClose;
        _channel.OnError = handleChannelError;

        if((lastMessageSent != null) && !nonACKed.Contains(lastMessageSent))
        {
            nonACKed.Add(lastMessageSent);
        }
    }

    private void handleChannelMessage(byte[] bytes)
    {
        string str = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        Log($"Message - \"{str}\"");

        // IF ACK message, remove from UnACKed and early out
        if (str.Substring(0, 5) == "(ACK)")
        {
            string message = str.Substring(5);
            nonACKed.Remove(message);
            return;
        }

        if(receivedMessages.Contains(str))
        {
            return;
        }
        else
        {
            receivedMessages.Add(str);
        }

        // Split message & call appropriate handler
        int index = str.IndexOf('-');
        string name = str.Substring(0, index);
        string command = str.Substring(index + 1);

        // Alert listener to message
        Action<string> callback;
        bool success = _listeners.TryGetValue(name, out callback);
        if (success)
        {
            callback?.Invoke(command);
        }
        else
        {
            Log($"Unregistered message - \"{name}-{command}\"");
        }
    }

    private void handleChannelOpen()
    {
        Log("WebRTC data channel open");
    }

    private void handleChannelClose()
    {
        Log("WebRTC data channel closed");
    }

    private void handleChannelError(RTCError e)
    {
        Log($"WebRTC data channel Error: {e}");
    }

    private void Log(string msg)
    {
        Debug.Log($"<color=#00F0F0>Connection:</color> {msg}");
    }

    private void OnDestroy()
    {
        Log("Destroying socket.");
        _socket.Disconnect();
        _socket.Dispose();
    }
}

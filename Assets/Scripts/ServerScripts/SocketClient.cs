using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;

public class SocketClient : MonoBehaviour
{
    public int port;

    public static SocketIOUnity Socket
    {
        get
        {
            return _socket;
        }
    }
    private static SocketIOUnity _socket;

    public static string RoomID
    {
        get
        {
            return _roomID;
        }
    }
    private static string _roomID;

    //private bool minigamePlayed = false;
    [HideInInspector]
    public static Action<string> onRoomChange;

    void Awake()
    {
        Debug.Log("Initializing socket...");

        var uri = new Uri($"http://localhost:{port}/");
        _socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Query = new Dictionary<string, string>
            {
                {"token", "UNITY" }
            },
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });
        _socket.JsonSerializer = new NewtonsoftJsonSerializer();


        _socket.OnConnected += (sender, e) =>
        {
            //socket.Emit("Device", "Unity");
        };
        _socket.OnDisconnected += (sender, e) =>
        {
            Debug.Log("disconnect: " + e);
        };
        _socket.OnReconnectAttempt += (sender, e) =>
        {
            Debug.Log($"{DateTime.Now} Reconnecting: attempt = {e}");
        };

        _socket.On("UUID", (response) => {
            _roomID = response.GetValue<string>();
            onRoomChange?.Invoke(_roomID);
        });
        /*
        socket.On("PlayerConnect", (response) =>
        {
            var obj = response.GetValue<string>();
            if (!minigamePlayed)
            {
                minigamePlayed = true;
                socket.Emit("MiniGameStart", "game1");
            }
            Debug.Log(obj);
        });
        
        socket.On("PhoneFaceUp", (response) =>
        {
            var obj = response.GetValue<string>();
            Debug.Log(obj);
            LivingRoomManager.state = LivingRoomState.PlayingGame;
            PhoneClicked.wasPaused = true;
        });

        socket.On("PhoneFaceDown", (response) =>
        {
            //var obj = response.GetValue<string>();
            //Debug.Log(obj);
            LivingRoomManager.state = LivingRoomState.GamePaused;
        });

        socket.On("MiniGameEnd", (response) =>
        {
            //var obj = response.GetValue<string>();
            //Debug.Log(obj);
            PhoneFoundDialogue.photoFound = true;
        });*/
        
        Debug.Log("Connecting Socket...");
        _socket.Connect();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnDestroy()
    {
        Debug.Log("Destroying socket.");
        _socket.Disconnect();
        _socket.Dispose();
    }
}

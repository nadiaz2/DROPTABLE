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

    private SocketIOUnity socket;
    private bool minigamePlayed = false;

    void Awake() {
        Debug.Log("Initializing socket...");
        
        var uri = new Uri($"http://localhost:{port}/");
        socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Query = new Dictionary<string, string>
            {
                {"token", "UNITY" }
            },
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });
        socket.JsonSerializer = new NewtonsoftJsonSerializer();


        socket.OnConnected += (sender, e) =>
        {
            socket.Emit("Device", "Unity");
        };
        socket.OnDisconnected += (sender, e) =>
        {
            Debug.Log("disconnect: " + e);
        };
        socket.OnReconnectAttempt += (sender, e) =>
        {
            Debug.Log($"{DateTime.Now} Reconnecting: attempt = {e}");
        };

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
            GameManager.state = GameState.PlayingGame;
            PhoneClicked.wasPaused = true;
        });

        socket.On("PhoneFaceDown", (response) =>
        {
            //var obj = response.GetValue<string>();
            //Debug.Log(obj);
            GameManager.state = GameState.GamePaused;
        });

        socket.On("MiniGameEnd", (response) =>
        {
            //var obj = response.GetValue<string>();
            //Debug.Log(obj);
            PhoneFoundDialogue.photoFound = true;
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting Socket...");
        socket.Connect();
    }

    void OnDestroy() {
        Debug.Log("Destroying socket.");
        socket.Disconnect();
        socket.Dispose();
    }
}

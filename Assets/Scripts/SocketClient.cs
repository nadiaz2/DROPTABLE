using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;
using WebSockets;

public class SocketClient : MonoBehaviour
{
    private SocketIOUnity socket;

    private bool minigamePlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hi");
        var uri = new Uri($"http://localhost:8001/");
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
            Debug.Log("after connect");
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
        });

        socket.On("PhoneFaceDown", (response) =>
        {
            var obj = response.GetValue<string>();
            Debug.Log(obj);
        });

        socket.On("MiniGameEnd", (response) =>
        {
            //var obj = response.GetValue<string>();
            //Debug.Log(obj);
            PhoneFoundDialogue.photoFound = true;
        });

        Debug.Log("before connect");
        socket.Connect();
        Debug.Log("boop");
    }
}

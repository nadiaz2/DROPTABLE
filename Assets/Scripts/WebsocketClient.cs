using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using WebSocketSharp;
using JSONObjects;

public class WebsocketClient : MonoBehaviour
{
    private WebSocket ws;

    // Start is called before the first frame update
    void Start()
    {
        ws = new WebSocket($"ws://localhost:8000/unity");
        ws.OnMessage += (sender, e) => OnMessage(e);
        ws.Connect();
    }

    void OnMessage(MessageEventArgs e)
    {
        Debug.Log(e.Data);
        MessageObject messageData = JsonUtility.FromJson<MessageObject>(e.Data);
        Debug.Log(messageData.name);

        if (messageData.name == "connect")
        {
            ws.Send($"{{\"name\": \"MiniGameStart\", \"message\": 1}}");
        }
    }
}

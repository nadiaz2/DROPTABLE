using UnityEngine;

using WebSocketSharp;
using WebSocketSharp.Server;

using JSONObjects;

namespace WebSocketBehaviors {
    public class HandleMessage : WebSocketBehavior
    {
        protected override void OnMessage (MessageEventArgs e)
        {
            MessageObject messageData = JsonUtility.FromJson<MessageObject>(e.Data);
            Debug.Log(messageData.name);

            if(messageData.name == "connect")
            {
                Send($"{{\"name\": \"MiniGameStart\", \"message\": 1}}");
            }
        }
    }
}
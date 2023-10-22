using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace My_Websocket {
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage (MessageEventArgs e)
        {
            Send (e.Data);
        }
    }
}
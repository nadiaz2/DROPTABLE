using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebSocketBehaviors {
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage (MessageEventArgs e)
        {
            Send (e.Data);
        }
    }
}
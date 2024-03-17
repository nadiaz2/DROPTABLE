using UnityEngine;

public class CommunicationControllerImpl : CommunicationController
{
    //Note: input is not supported
    //public override void InputData(string message) {}

    public override void OutputData(string message)
    {
        Debug.Log($"Node.JS: {message}");
    }

    public override void ErrorData(string message)
    {
        Debug.LogWarning($"Node.JS Error: {message}");
    }
}

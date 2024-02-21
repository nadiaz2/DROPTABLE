using UnityEngine;

public class CommunicationControllerImpl : CommunicationController
{
    //Note: input is not supported
    //public override void InputData(string message) {}

    public override void OutputData(string message)
    {
        Debug.Log(message);
    }

    public override void ErrorData(string message)
    {
        Debug.Log($"ERROR: {message}");
    }
}

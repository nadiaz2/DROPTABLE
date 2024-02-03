using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public enum TomsRoomState
{
    Start,
    PlayingGame,
    GamePaused,
}

public class TomsRoomManager : MonoBehaviour
{

    public static TomsRoomState state { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        state = TomsRoomState.Start;
    }
}

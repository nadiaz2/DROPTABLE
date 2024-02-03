using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public enum JacobsRoomState
{
    Start,
    PlayingGame,
    GamePaused,
}

public class JacobsRoomManager : MonoBehaviour
{

    public static JacobsRoomState state { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        state = JacobsRoomState.Start;
    }
}

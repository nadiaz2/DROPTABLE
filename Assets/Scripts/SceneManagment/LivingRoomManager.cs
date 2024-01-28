using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public enum LivingRoomState
{
    Start,
    TalkingToJacob,
    FinishedTalking,
    PlayingGame,
	GamePaused,
}

public class LivingRoomManager : MonoBehaviour
{
    public GameObject jacob;
    public GameObject headphones;

    public static LivingRoomState state { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        state = LivingRoomState.Start;
    }
}

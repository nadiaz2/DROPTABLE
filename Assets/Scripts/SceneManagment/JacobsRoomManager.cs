using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public enum JacobsRoomState
{
    Start,
    PlayingGame,
    GamePaused,

    // Day 1
    Day1Start
}

public class JacobsRoomManager : MonoBehaviour
{

    public BlackScreen blackScreen;

    public static JacobsRoomState state { get; set; }

    // Start is called before the first frame update
    void Start()
    {

        blackScreen.goBlacked = false;

        
        switch (GameManager.state)
        {
            case GameState.Day1LivingRoomStart:
                state = JacobsRoomState.Day1Start;
                break;
        }
    }

    public void FadeOut()
    {
        if (blackScreen != null)
        {
            blackScreen.goBlacked = true;
        }
    }

    public void FadeIn()
    {
        if (blackScreen != null)
        {
            blackScreen.goBlacked = false;
        }
    }
}

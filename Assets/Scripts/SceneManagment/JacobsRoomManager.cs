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
    Day1Start,

    // Day 4
    Day4SearchJacobsRoom,
    Day4FoundKey,
}

public class JacobsRoomManager : MonoBehaviour
{
    public JacobsDrawer jacobsDrawer;

    public SubtitleTrigger subtitleTrigger;

    private bool triggered = false;

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

            case GameState.Day4StartTomsRoom:
                state = JacobsRoomState.Day4SearchJacobsRoom;
                jacobsDrawer.interactable = true;
                break;

        }

    }

    private void Update()
    {
        switch (JacobsRoomManager.state)
        {
            case JacobsRoomState.Day4FoundKey:
                if (!triggered)
                {
                    subtitleTrigger.TriggerSubtitle(() =>
                    {
                        GameManager.state = GameState.Day4HeadToJacobsLab;
                    });
                    triggered = true;
                }

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

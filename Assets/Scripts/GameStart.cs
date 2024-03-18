using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public GameState startState;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.state = startState;
    }

    // Update is called once per frame
    void Update()
    {
        switch (startState)
        {
            case GameState.GameStart:
                SceneManager.LoadScene("Classroom");
                break;
            case GameState.ClassRoomStart:
                SceneManager.LoadScene("Classroom");
                break;
            case GameState.OnWayHomeStart:
                SceneManager.LoadScene("OnWayHome");
                break;
            case GameState.BackToClassroom:
                SceneManager.LoadScene("Classroom");
                break;
            case GameState.ReturningHomeAfterHeadphones:
                SceneManager.LoadScene("LivingRoom");
                break;
            case GameState.Day1LivingRoomStart:
                SceneManager.LoadScene("LivingRoom");
                break;
            case GameState.Day1JacobsBack:
                SceneManager.LoadScene("LivingRoom");
                break;

            // Day 2
            case GameState.Day2StartTomsRoom:
                SceneManager.LoadScene("TomsRoom");
                break;
            case GameState.Day2HeadBackToSchool:
                break;
            case GameState.Day2AfternoonClass:
                SceneManager.LoadScene("Classroom");
                break;
            case GameState.Day2HeadBackHome:
                break;

            // Day 3
            case GameState.Day3StartTomsRoom:
                break;
            case GameState.Day3FinishedMiniGame:
                break;
            case GameState.Day3TalkedWithJacob:
                break;
            case GameState.Day3InBackBay:
                break;
            case GameState.Day3InClothingStore:
                break;
            case GameState.Day3End:
                SceneManager.LoadScene("JacobsCar");
                break;

            // Day 4
            case GameState.Day4StartTomsRoom:
                SceneManager.LoadScene("TomsRoom");
                break;

            case GameState.Day4HeadToJacobsLab:
                SceneManager.LoadScene("SchoolCorridor");
                break;

            case GameState.Day4InLab:
                SceneManager.LoadScene("Lab");
                break;

            case GameState.Day4JacobFlashBack:
                SceneManager.LoadScene("JacobsRoom");
                break;
        }
    }
}

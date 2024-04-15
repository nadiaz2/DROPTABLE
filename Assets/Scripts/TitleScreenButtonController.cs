using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButtonController : MonoBehaviour
{
    public BlackScreen blackScreen;

    public void Awake()
    {
        blackScreen.goBlacked = false;
    }

    public void StartNewGame()
    {
        GameManager.cameraMove = false;
        GameManager.day1BranchEndGame = false;
        GameManager.day3BranchRomanticRoute = false;
        GameManager.lastScene = "";

        GameManager.day1Started = false;
        GameManager.day2Started = false;
        GameManager.day3Started = false;
        GameManager.day4Started = false;

        GameManager.state = GameState.GameStart;

        blackScreen.goBlacked = true;
        Invoke("ChangeScene", 1.0f);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("Classroom");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}

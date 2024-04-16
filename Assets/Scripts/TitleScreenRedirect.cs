using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenRedirect : MonoBehaviour
{
    public float neededHoldTime;
    private float secondsPressed;

    void Start()
    {
        secondsPressed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Equals))
        {
            secondsPressed += Time.deltaTime;
        }
        else
        {
            secondsPressed = 0.0f;
        }

        if(secondsPressed >= neededHoldTime)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("TitleScreen");
        }
    }
}

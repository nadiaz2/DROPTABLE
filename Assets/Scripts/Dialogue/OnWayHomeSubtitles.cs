using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWayHomeSubtitles : MonoBehaviour
{

    public DialogueTrigger trigger;

    private bool firstTime = true;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.state = GameState.OnWayHomeStart;
        if (GameManager.state == GameState.OnWayHomeStart && firstTime)
        {
            Invoke("playSubtitle", 5);
            firstTime = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void playSubtitle()
    {
        trigger.TriggerDialogue();
    }
}

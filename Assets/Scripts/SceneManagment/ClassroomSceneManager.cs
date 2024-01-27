using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

enum ClassroomState
{
    // States for first time in classroom
    Start,
    Seated,
    SubtitleEnd,
    ClassOver,

    // States for returning to pick up headphones
    Return,
    PuckedUpHeadphones,
    MorganCloseUp,
    AfterHeadphones
}

public class ClassroomSceneManager : MonoBehaviour
{
    public GameObject jacob;
    public GameObject headphones;

    // Start is called before the first frame update
    void Start()
    {
        // Returning to classroom to pick up headphones
        if (GameManager.state == GameState.BackToClassroom)
        {
            jacob.SetActive(false);
            headphones.SetActive(true);
        }
    }

    /*
    public DialogueTrigger trigger;
    public GameObject jacob;
    public GameObject headphones;

    private bool triggerStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"{GameManager.state}");
        if (GameManager.state == GameState.BackToClassroom)
        {
            jacob.SetActive(false);
            headphones.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.PickedupHeadphones && !triggerStarted)
        {
            trigger.TriggerDialogue();
            triggerStarted = true;
        }
    }
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[System.Flags]
public enum ClassroomState
{
    // States for first time in classroom
    Start = 1,
    Seated = 2,
    //FinishedTalking,
//SubtitleEnd,
    ClassOver = 4,

    // States for returning to pick up headphones
    Return = 8,
    PickedUpHeadphones = 16,
    MorganCloseUp = 32,
    AfterHeadphones = 64
}

public class ClassroomManager : MonoBehaviour
{
    public GameObject jacob;
    public Headphones headphones;

    public BlackScreen blackScreen;

    public static ClassroomState state { get; set; }
    public static ClassroomManager currentInstance
    {
        get
        {
            return _instance;
        }
    }
    private static ClassroomManager _instance;

    // Start is called before the first frame update
    void Start()
    {
        ClassroomManager._instance = this;

        blackScreen.goBlacked = false;

        // Returning to classroom to pick up headphones
        if (GameManager.state == GameState.BackToClassroom)
        {
            jacob.SetActive(false);
            headphones.gameObject.SetActive(true);
            headphones.interactable = true;
            state = ClassroomState.Return;
        }
        else
        {
            state = ClassroomState.Start;
        }
    }

    public void FadeOut()
    {
        if(blackScreen != null)
        {
            blackScreen.goBlacked = true;
        }
    }

    public void FadeIn()
    {
        if(blackScreen != null)
        {
            blackScreen.goBlacked = false;
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

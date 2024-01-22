using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ClassroomSelfDialogue : MonoBehaviour
{

    public DialogueTrigger trigger;
    public DialogueTrigger trigger2;

    private bool trigger2Started = false;


    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.state == GameState.BackToClassroom)
        {
            gameObject.SetActive(false);
            return;
        }

        GameManager.state = GameState.ClassRoomStart;
        trigger.TriggerDialogue();
    }

    void Update()
    {
        if (GameManager.state == GameState.ClassOver && !trigger2Started)
        {
            trigger2.TriggerDialogue();
            trigger2Started = true;
        }
    }


}

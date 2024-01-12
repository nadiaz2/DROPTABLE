using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomSelfDialogue : MonoBehaviour
{

    public DialogueTrigger trigger;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.state = GameState.ClassRoomSubtitleStart;
        trigger.TriggerDialogue();
    }

}

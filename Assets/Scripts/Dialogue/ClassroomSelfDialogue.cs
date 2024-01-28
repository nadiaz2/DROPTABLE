using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ClassroomSelfDialogue : MonoBehaviour
{

    public SubtitleTrigger trigger;
    public SubtitleTrigger trigger2;

    private bool trigger2Started = false;


    // Start is called before the first frame update
    void Start()
    {
        if (ClassroomManager.state == ClassroomState.Return)
        {
            gameObject.SetActive(false);
            return;
        }

        trigger.TriggerSubtitle();
    }

    void Update()
    {
        if (ClassroomManager.state == ClassroomState.ClassOver && !trigger2Started)
        {
            trigger2.TriggerSubtitle();
            trigger2Started = true;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ClassroomSceneManager : MonoBehaviour
{
    public DialogueTrigger trigger;

    public GameObject jacob;
    public GameObject headphones;

    private bool triggerStarted = false;

    // Start is called before the first frame update
    void Start()
    {
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
}

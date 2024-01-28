using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JacobClassroomInteractable : MonoBehaviour, Interactable
{
    public DialogueTrigger trigger;

    private ClassroomState lastState;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        lastState = ClassroomManager.state;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ClassroomManager.state == lastState)
        {
            return;
        }

        switch(ClassroomManager.state)
        {
            case ClassroomState.Seated:
                this.active = true;
                break;
            default:
                this.active = false;
                break;
        }

        lastState = ClassroomManager.state;
    }

    private IEnumerator ChangeSceneState(float waitTime,ClassroomState state)
    {
        yield return new WaitForSeconds(waitTime);
        ClassroomManager.state = state;
    }

    public void Interact()
    {
        trigger.TriggerDialogue(() => {
            this.active = false;
            ClassroomManager.currentInstance.Invoke("FadeOut", 1);
            StartCoroutine(ChangeSceneState(3.5f, ClassroomState.ClassOver));
            ClassroomManager.currentInstance.Invoke("FadeIn", 4);
        });
    }

    public string GetPrompt()
    {
        return "Talk to Jacob";
    }

    public bool IsActive()
    {
        return this.active;
    }
}

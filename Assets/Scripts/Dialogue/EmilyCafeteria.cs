using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmilyCafeteria : MonoBehaviour, Interactable
{

    public SubtitleTrigger trigger;
    public GameObject rachelPhoto;
    public GameObject player;

    public bool interactable = false;
    private bool triggerStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (CafeteriaManager.state)
        {
            case CafeteriaState.Day2EmilyPhone:
                if (!triggerStarted)
                {
                    trigger.TriggerSubtitle();
                    interactable = true;
                    triggerStarted = true;
                    CafeteriaManager.state = CafeteriaState.Day2SeenPhoto;
                }
                break;

            case CafeteriaState.Day2SeenPhoto:
                if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                {
                    rachelPhoto.SetActive(false);
                    player.gameObject.SetActive(true);
                }
                break;
        }
    }

    public void Interact()
    {
        this.interactable = false;
        rachelPhoto.SetActive(true);
        player.gameObject.SetActive(false);

    }

    public string GetPrompt()
    {
        return "Look at Photo";
    }

    public bool IsActive()
    {
        return this.interactable;
    }
}

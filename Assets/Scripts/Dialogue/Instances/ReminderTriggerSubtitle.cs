using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReminderTriggerSubtitle : MonoBehaviour
{
    public SubtitleTrigger subtitleTrigger;
    public SubtitleTrigger subtitleTriggerDay4Reminder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GoToClassReminder" && GameManager.state == GameState.Day2HeadBackToSchool)
        {
            subtitleTrigger.TriggerSubtitle();
        }
        else if (other.gameObject.tag == "GoToClassReminder" && GameManager.state == GameState.Day4HeadToJacobsLab)
        {
            subtitleTriggerDay4Reminder.TriggerSubtitle();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReminderTriggerSubtitle : MonoBehaviour
{
    public SubtitleTrigger subtitleTrigger;

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
    }
}

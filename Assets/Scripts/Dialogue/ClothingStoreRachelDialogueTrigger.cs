using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClothingStoreRachelDialogueTrigger : MonoBehaviour
{

    public SubtitleTrigger subtitleTrigger;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RachelDialogueTrigger")
        {
            Debug.Log("Here");
            subtitleTrigger.TriggerSubtitle();
        }
    }

}

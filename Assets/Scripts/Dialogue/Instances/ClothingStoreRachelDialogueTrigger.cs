using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClothingStoreRachelDialogueTrigger : MonoBehaviour
{

    public SubtitleTrigger subtitleTrigger;
    public SubtitleTrigger subtitleTrigger2;

    public SplineMovement manMovement;
    public SplineMovement rachelMovement;

    public Animator manAnimator;
    public Animator rachelAnimator;

    public GameObject womensIsleTrigger;

    private bool trigger1Started;
    private bool trigger2Started;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RachelDialogueTrigger")
        {
            if (!trigger1Started)
            {
                trigger1Started = true;
                subtitleTrigger.TriggerSubtitle(() =>
                {
                    womensIsleTrigger.SetActive(true);

                    manMovement.StartMovement(() =>
                    {
                        manAnimator.SetBool("IsWalking", false);
                        manAnimator.SetBool("IsStanding", true);
                    });
                    rachelMovement.StartMovement(() =>
                    {
                        rachelAnimator.SetBool("IsWalking", false);
                        rachelAnimator.SetBool("IsStanding", true);
                    });

                    manAnimator.SetBool("IsWalking", true);
                    rachelAnimator.SetBool("IsWalking", true);
                });
            }

        }
        if (other.gameObject.tag == "WomensIsle")
        {
            if (!trigger2Started)
            {
                trigger2Started = true;
                subtitleTrigger2.TriggerSubtitle(() =>
                {
                    Invoke("changeStateToEndFlashBack", 2);
                });
            }
        }
    }

    private void changeStateToEndFlashBack()
    {
        ClothingStoreManager.state = ClothingStoreState.Day3EmilyFlashBackFinished;
    }

}

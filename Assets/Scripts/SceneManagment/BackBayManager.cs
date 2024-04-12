using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum BackBayState
{
    Start,

    //Day 3
    Day3GoInsideStore,
    Day3BackBayEnd,
}

public class BackBayManager : MonoBehaviour
{
    public GameObject jacobsCarSceneTrigger;
    public PlayerMovement2D player;
    public Animator jacob;

    public SubtitleTrigger subtitleTrigger;
    public SubtitleTrigger subtitleTriggerHeadBackHome;


    public BlackScreen blackScreen;

    public static BackBayState state { get; set; }
    public static BackBayManager currentInstance
    {
        get
        {
            return _instance;
        }
    }
    private static BackBayManager _instance;

    // Start is called before the first frame update
    void Start()
    {
        BackBayManager._instance = this;

        blackScreen.goBlacked = false;


        switch (GameManager.state)
        {
            case GameState.Day3InBackBay:
                Invoke("startSubtitle", 3);
                break;

            case GameState.Day3End:
                subtitleTriggerHeadBackHome.TriggerSubtitle(() =>
                {
                    jacobsCarSceneTrigger.SetActive(true);
                });
                break;
        }

    }


    void Update()
    {
        switch (GameManager.state)
        {
            case GameState.Day3InBackBay:
            case GameState.Day3End:
            {
                Vector3 newPos = jacob.transform.position;
                newPos.z = player.transform.position.z;

                Vector3 facing = player.rb.velocity;
                facing.y = 0;
                newPos -= 0.08f * facing;


                jacob.transform.position = newPos;
                jacob.transform.rotation = player.transform.rotation;

                bool moving = (facing != Vector3.zero);
                jacob.SetBool("IsWalking", moving);
                jacob.SetBool("IsStanding", !moving);
                break;
            }
        }
    }

    private void startSubtitle()
    {
        subtitleTrigger.TriggerSubtitle(() =>
        {
            BackBayManager.state = BackBayState.Day3GoInsideStore;
        });
    }


    public void FadeOut()
    {
        if (blackScreen != null)
        {
            blackScreen.goBlacked = true;
        }
    }

    public void FadeIn()
    {
        if (blackScreen != null)
        {
            blackScreen.goBlacked = false;
        }
    }

}
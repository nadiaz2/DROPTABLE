using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[System.Flags]
public enum ClothingStoreState
{
    Start,

    //Day 3
    Day3InsideClothingStore,
    Day3StartYellowDressDialogue,
    Day3EmilyFlashBack,
}

public class ClothingStoreManager : MonoBehaviour
{
    public GameObject player;
    public GameObject playerEmily;
    public GameObject rachel;
    public GameObject rachelDialogueTrigger;

    public SubtitleTrigger subtitleTrigger;
    public SubtitleTrigger subtitleTriggerItem1;
    public SubtitleTrigger subtitleTriggerItem2;
    public SubtitleTrigger subtitleTriggerItem3;
    public SubtitleTrigger flashBackSubtitleTrigger;
    public GuessingInteractable item1;
    public GuessingInteractable item2;
    public GuessingInteractable item3;
    public ClothingStoreYellowDress yellowDress;

    public BlackScreen blackScreen;

    public int itemCount;

    public bool itemsActive = true;
    private bool item1SubtitleTriggered = false;
    private bool item2SubtitleTriggered = false;
    private bool item3SubtitleTriggered = false;
    private bool flashBackSubtitleTriggered = false;

    public static ClothingStoreState state { get; set; }
    public static ClothingStoreManager currentInstance
    {
        get
        {
            return _instance;
        }
    }
    private static ClothingStoreManager _instance;

    // Start is called before the first frame update
    void Start()
    {
        ClothingStoreManager._instance = this;

        blackScreen.goBlacked = false;


        if (GameManager.state == GameState.GameStart)
        {
            GameManager.state = GameState.Day3InBackBay;
        }

        switch (GameManager.state)
        {
            case GameState.Day3InBackBay:
                GameManager.state = GameState.Day3InClothingStore;
                Invoke("startSubtitle", 2);
                break;
        }

    }


    private void Update()
    {

        switch (ClothingStoreManager.state)
        {
            case ClothingStoreState.Day3InsideClothingStore:
                if (itemCount == 1 && !item1SubtitleTriggered && itemsActive)
                {
                    item1SubtitleTriggered = true;
                    itemsActive = false;
                    subtitleTriggerItem1.TriggerSubtitle(() =>
                    {
                        itemsActive = true;
                    });
                }
                if (itemCount == 2 && !item2SubtitleTriggered && itemsActive)
                {
                    item2SubtitleTriggered = true;
                    itemsActive = false;
                    subtitleTriggerItem2.TriggerSubtitle(() =>
                    {
                        itemsActive = true;
                    });
                }
                if (itemCount == 3 && !item3SubtitleTriggered && itemsActive)
                {
                    item3SubtitleTriggered = true;
                    itemsActive = false;
                    subtitleTriggerItem3.TriggerSubtitle(() =>
                    {
                        itemsActive = true;
                        yellowDress.interactable = true;
                    });
                }
                break;

            case ClothingStoreState.Day3EmilyFlashBack:
                Invoke("FadeOut", 0.1f);
                Invoke("FadeIn", 2);
                Invoke("setUpFalshBackCharacters", 2);
                Invoke("startFlashBackSubtitle", 4);
                break;
        }
    }

    private void setUpFalshBackCharacters()
    {
        player.SetActive(false);
        playerEmily.SetActive(true);
    }

    private void startSubtitle()
    {
        subtitleTrigger.TriggerSubtitle(() =>
        {
            state = ClothingStoreState.Day3InsideClothingStore;
        });
    }

    private void startFlashBackSubtitle()
    {
        if (!flashBackSubtitleTriggered)
        {
            flashBackSubtitleTrigger.TriggerSubtitle(() =>
            {
                rachelDialogueTrigger.SetActive(true);
                rachel.SetActive(true);
            });
            flashBackSubtitleTriggered = true;
        }
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
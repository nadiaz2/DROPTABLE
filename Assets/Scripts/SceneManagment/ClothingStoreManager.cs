using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Flags]
public enum ClothingStoreState
{
    Start,

    //Day 3
    Day3InsideClothingStore,
    Day3StartYellowDressDialogue,
    Day3EmilyFlashBack,
    Day3EmilyFlashBackFinished,
}

public class ClothingStoreManager : MonoBehaviour
{
    [Header("Objects")]
    public GameObject player;
    public GameObject playerEmily;
    public GameObject rachel;
    public GameObject man;
    public GameObject rachelDialogueTrigger;
    public GameObject emily;
    public GameObject emilyAfterFlashBack;
    public GameObject jacob;

    [Header("Triggers")]
    public SubtitleTrigger subtitleTrigger;
    public SubtitleTrigger subtitleTriggerItem1;
    public SubtitleTrigger subtitleTriggerItem2;
    public SubtitleTrigger subtitleTriggerItem3;
    public SubtitleTrigger flashBackSubtitleTrigger;
    public DialogueTrigger dialogueTrigger;

    [Header("Interactables")]
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
    private bool dialogueTriggerStarted = false;

    public static ClothingStoreState state { get; set; }
    public static ClothingStoreManager currentInstance
    {
        get
        {
            return _instance;
        }
    }
    private static ClothingStoreManager _instance;

    private ClothingStoreState lastState;

    // Start is called before the first frame update
    void Start()
    {
        ClothingStoreManager._instance = this;

        blackScreen.goBlacked = false;

        switch (GameManager.state)
        {

            case GameState.Day3InClothingStore:
                Invoke("startSubtitle", 2);
                break;
        }

    }


    private void Update()
    {
        // Only triggers on state change
        if(ClothingStoreManager.state != lastState)
        {
            lastState = ClothingStoreManager.state;
            switch (ClothingStoreManager.state)
            {
                case ClothingStoreState.Day3EmilyFlashBack:
                    ClothingStoreManager.currentInstance.Invoke("FadeOut", 1);
                    ClothingStoreManager.currentInstance.Invoke("FadeIn", 4);
                    Invoke("setUpFlashBackCharacters", 3);
                    Invoke("startFlashBackSubtitle", 4);
                    break;

                case ClothingStoreState.Day3EmilyFlashBackFinished:
                    FadeOut();
                    Invoke("FadeIn", 4.0f);
                    Invoke("setUpAfterFlashBack", 2.0f);
                    break;
            }
        }

        // Triggers contantly for state
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

            case ClothingStoreState.Day3EmilyFlashBackFinished:
                if (dialogueTriggerStarted)
                {
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                    {
                        dialogueTrigger.ContinueDialogue();
                    }
                }
                break;
        }
    }

    private void setUpFlashBackCharacters()
    {
        player.SetActive(false);
        emily.SetActive(false);
        jacob.SetActive(false);
        playerEmily.SetActive(true);
    }

    private void setUpAfterFlashBack()
    {
        playerEmily.SetActive(false);
        man.SetActive(false);
        rachel.SetActive(false);
        player.SetActive(true);
        emilyAfterFlashBack.SetActive(true);
        jacob.SetActive(true);

        if (!dialogueTriggerStarted)
        {
            dialogueTrigger.TriggerDialogue(() =>
            {
                Invoke("backOutToBackBay", 2);
            });
            dialogueTriggerStarted = true;
        }
        
    }

    private void backOutToBackBay()
    {
        SceneManager.LoadScene("BackBay");
        GameManager.state = GameState.Day3End;
        BackBayManager.state = BackBayState.Day3BackBayEnd;
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
                man.SetActive(true);
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
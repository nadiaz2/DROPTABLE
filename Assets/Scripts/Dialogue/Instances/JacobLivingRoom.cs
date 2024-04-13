using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JacobLivingRoom : MonoBehaviour, Interactable
{

    [Header("Game Objects")]
    public LivingRoomPhone phone;
    public Camera mainCamera;
    public Transform oppositeCameraPosition;
    public PlayerMovement player;

    [Header("Day 1")]
    public DialogueTrigger jacobReturnDialogue;
    public SubtitleTrigger foodArrivedSubtitle;
    public SplineMovement getFoodPath;
    public SplineMovement returnFromFoodPath;
    public DialogueTrigger backFromFoodDialogue;
    public ChoiceTrigger day1Choice;

    [Header("Day 2")]
    public DialogueTrigger day2ReturnDialogue;


    [Header("Day 3")]
    public DialogueTrigger dialogueTriggerDay3;

    [Header("Animator")]
    public Animator animator;

    [HideInInspector]
    public bool interactable = false;
    private bool dialogueTriggerStarted = false;
    private bool dialogueTriggerDay3Started = false;
    private bool changedCamera = false;

    private LivingRoomState lastState;

    public void Start()
    {
        lastState = LivingRoomManager.state;
    }

    public void Update()
    {
        if (LivingRoomManager.state != lastState)
        {
            lastState = LivingRoomManager.state;

            switch (LivingRoomManager.state)
            {
                case LivingRoomState.Day1FoundPhoto:
                    returnFromFoodPath.StartMovement(() =>
                    {
                        animator.SetBool("IsWalking", false);
                        animator.SetBool("IsStanding", true);
                    });
                    interactable = true;
                    backFromFoodDialogue.TriggerDialogue(() =>
                    {
                        interactable = false;
                        player.immobile = true;
                        day1Choice.PresentChoice((int choiceIndex) =>
                        {
                            if (choiceIndex == 0)
                            {
                                GameManager.day1BranchEndGame = true;
                                GameManager.state = GameState.Day1GameEndChoice;

                                Debug.Log($"End Game: {choiceIndex}");
                            }
                            else if (choiceIndex == 1)
                            {
                                GameManager.day1BranchEndGame = false;
                                GameManager.state = GameState.Day1GameEndChoice;

                                Debug.Log($"Continue Game: {choiceIndex}");
                                return;
                            }
                        });
                    });
                    break;

                case LivingRoomState.Day2JacobsReturned:
                    returnFromFoodPath.StartMovement(() =>
                    {
                        animator.SetBool("IsWalking", false);
                        animator.SetBool("IsStanding", true);
                    });
                    animator.SetBool("IsWalking", true);

                    day2ReturnDialogue.TriggerDialogue(() =>
                    {
                        LivingRoomManager.currentInstance.FadeOut();
                        Invoke("EndDay", 1.0f);
                    });
                    break;
            }


        }
    }

    private void EndDay()
    {
        GameManager.state = GameState.Day3StartTomsRoom;
        SceneManager.LoadScene("TomsRoom");
    }

    public void EndStudySession()
    {
        foodArrivedSubtitle.TriggerSubtitle(() =>
        {
            getFoodPath.StartMovement();
            phone.active = true;
            animator.SetBool("IsSitting", false);
            animator.SetBool("IsWalking", true);
        });
    }

    public void sitCharacter()
    {
        animator.SetBool("IsSitting", true);
        player.seated = true;
    }

    public void Interact()
    {
        switch (LivingRoomManager.state)
        {
            case LivingRoomState.Day1JacobsBackAfterBed:
                if (!dialogueTriggerStarted)
                {
                    jacobReturnDialogue.TriggerDialogue(() =>
                    {
                        this.interactable = false;
                        LivingRoomManager.state = LivingRoomState.Day1TalkedWithJacob;

                        LivingRoomManager manager = LivingRoomManager.currentInstance;
                        manager.FadeOut();
                        Invoke("cameraChange", 2.5f);
                        manager.Invoke("SitCharacters", 2.5f);
                        Invoke("sitCharacter", 2.5f);
                        Invoke("EndStudySession", 2.7f);
                        manager.Invoke("FadeIn", 3.0f);
                    });
                    dialogueTriggerStarted = true;
                }
                else
                {
                    jacobReturnDialogue.ContinueDialogue();
                }
                break;

            case LivingRoomState.Day1FoundPhoto:
                backFromFoodDialogue.ContinueDialogue();
                break;

            case LivingRoomState.Day3Start:

                break;
        }

        switch (GameManager.state)
        {
            case GameState.Day3FinishedMiniGame:
                if (!dialogueTriggerDay3Started)
                {
                    dialogueTriggerDay3.TriggerDialogue(() =>
                    {
                        this.interactable = false;
                        LivingRoomManager.state = LivingRoomState.Day3TalkedWithJacob;
                        GameManager.state = GameState.Day3TalkedWithJacob;
                    });
                    dialogueTriggerDay3Started = true;
                }
                else
                {
                    dialogueTriggerDay3.ContinueDialogue();
                }
                break;
        }
    }

    private void cameraChange()
    {
        if (!changedCamera)
        {
            if (LivingRoomManager.state == LivingRoomState.Day1TalkedWithJacob)
            {
                mainCamera.transform.SetPositionAndRotation(oppositeCameraPosition.position, oppositeCameraPosition.rotation);
            }
            changedCamera = true;
        }
    }

    public string GetPrompt()
    {
        return "Talk to Jacob";
    }

    public bool IsActive()
    {
        return this.interactable;
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    public PlayerMovement player;
    public PlayerMovement otherPlayableCharacters;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image characterPortriatImage;
    public Animator animator;
    public float textSpeed;
    public bool finished = false;

    private static bool _dialogueOngoing;
    public static bool dialogueOngoing {
        get {
            return _dialogueOngoing;
        }
    }

    // Start is called before the first frame update
    private void Start() {
        _dialogueOngoing = false;
    }

    private void Update() {
        if (_dialogueOngoing && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)))
        {
            //DisplayNextSentence();
        }
    }

    private Queue<Dialogue> slides = new Queue<Dialogue>();
    private Action callbackFunc;

    public void StartDialogue(Dialogue[] dialogues, Action callback = null)
    {
        animator.SetBool("isOpen", true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.immobile = true;
        if (otherPlayableCharacters != null && otherPlayableCharacters.isActiveAndEnabled)
        {
            otherPlayableCharacters.immobile = true;
        }

        _dialogueOngoing = true;

        this.callbackFunc = callback;
        foreach (Dialogue dialogue in dialogues)
        {
            this.slides.Enqueue(dialogue);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //Debug.Log($"EndDialogue {slides.Count}");
        if (slides.Count == 0)
        {
            EndDialogue();
            finished = true;
            return;
        }
        Dialogue slide = this.slides.Dequeue();


        this.nameText.text = slide.name;

        Image image = slide.characterPortrait;
        if (image != null)
        {
            this.characterPortriatImage.sprite = image.sprite;
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(slide.sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }


    void EndDialogue()
    {
        Debug.Log("End of conversation.");
        animator.SetBool("isOpen", false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _dialogueOngoing = false;
        player.immobile = false;
        if (otherPlayableCharacters != null && otherPlayableCharacters.isActiveAndEnabled)
        {
            otherPlayableCharacters.immobile = false;
        }


        this.callbackFunc?.Invoke();
        /*
        if(GameManager.state == GameState.TalkingToJacob) {
            GameManager.state = GameState.FinishedTalking;
        }
        */
    }
}

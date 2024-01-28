using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{

    public TextMeshProUGUI subTitleText;
    public Animator animator;
    public float textSpeed;

    private Queue<string> sentences = new Queue<string>();
    private Action callbackFunc;

    private void Update()
    {
        /*
        if (GameManager.state == GameState.ClassRoomSubtitleEnd)
        {
            EndDialogue();
            GameManager.state = GameState.ClassRoomSeated;
            return;
        }
        */
    }


    public void StartSubtitle(string[] subtitles, Action callback = null)
    {
        animator.SetBool("isOpen", true);
        //this.sentences.Clear();

        this.callbackFunc = callback;
        foreach (string sentence in subtitles)
        {
            this.sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (this.sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = this.sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        Invoke("DisplayNextSentence", 5);
    }

    IEnumerator TypeSentence(string sentence)
    {
        subTitleText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            subTitleText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }


    void EndDialogue()
    {
        //Debug.Log("End of conversation.");
        animator.SetBool("isOpen", false);
        this.callbackFunc?.Invoke();
        /*
        if(GameManager.state == GameState.OnWayHomeStart)
        {
            GameManager.state = GameState.BackToClassroom;
        }
        */
    }
}

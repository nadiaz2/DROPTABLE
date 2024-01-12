using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{

    public TextMeshProUGUI subTitleText;

    public Animator animator;

    public float textSpeed;
    private Queue<string> sentences = new Queue<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (GameManager.state == GameState.ClassRoomSubtitleEnd)
        {
            EndDialogue();
            GameManager.state = GameState.ClassRoomSeated;
            return;
        }
    }

    public void StartSubtitle(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        Debug.Log(sentences);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        Debug.Log(subTitleText.text);
        subTitleText.text = "";
        Debug.Log(subTitleText.text);
        foreach (char letter in sentence.ToCharArray())
        {
            subTitleText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }


    void EndDialogue()
    {
        Debug.Log("End of conversation.");
        animator.SetBool("isOpen", false);
        DialogueTrigger.dialogueStart = false;

    }

}

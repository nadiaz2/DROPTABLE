using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChoiceManager : MonoBehaviour
{
    [Header("Binary Choice Buttons")]
    [SerializeField] private GameObject group2;
    [SerializeField] private TextMeshProUGUI group2Button0;
    [SerializeField] private TextMeshProUGUI group2Button1;

    [Header("Trinary Choice Buttons")]
    [SerializeField] private GameObject group3;
    [SerializeField] private TextMeshProUGUI group3Button0;
    [SerializeField] private TextMeshProUGUI group3Button1;
    [SerializeField] private TextMeshProUGUI group3Button2;

    private Action<int> currentCallback;

    // Start is called before the first frame update
    void Start()
    {
        group2?.SetActive(false);
    }

    public void PresentChoice(string[] choices, Action<int> callback) {
        currentCallback = callback;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        switch(choices.Length) {
            case 2:
                group2Button0.text = choices[0];
                group2Button1.text = choices[1];
                group2.SetActive(true);
                break;
            case 3:
                group3Button0.text = choices[0];
                group3Button1.text = choices[1];
                group3Button2.text = choices[2];
                group3.SetActive(true);
                break;
            default:
                Debug.LogError($"Decisions of size {choices.Length} are not currently supported.");
                break;
        }
    }

    public void ChoiceMade(int choiceIndex) {
        group2?.SetActive(false);
        group3?.SetActive(false);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if(currentCallback != null) {
            currentCallback(choiceIndex);
        }

        currentCallback = null;
    }
}

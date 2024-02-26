using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChoiceTrigger : MonoBehaviour
{
    public ChoiceManager manager;
    public string[] choices;

    public void PresentChoice(Action<int> callback) {
        manager.PresentChoice(choices, callback);
    }
}

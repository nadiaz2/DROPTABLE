using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Dialogue
{

    public string[] names;

    public Image[] characterPortraits;

    [TextArea(7, 15)]
    public string[] sentences;


}

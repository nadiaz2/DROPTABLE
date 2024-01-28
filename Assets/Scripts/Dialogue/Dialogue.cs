using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Dialogue
{

    public string name;

    public Image characterPortrait;

    [TextArea(7, 15)]
    public string sentence;


}

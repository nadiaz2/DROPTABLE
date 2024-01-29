using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SubtitleTrigger : MonoBehaviour
{
    public SubtitleManager subtitleManager;

    [TextArea(7, 15)]
    public string[] subtitles;

    public void TriggerSubtitle(Action callback = null)
    {
        subtitleManager.StartSubtitle(subtitles, callback);
    }
}

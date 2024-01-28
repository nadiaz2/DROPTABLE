using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SubtitleTrigger : MonoBehaviour
{
    public SubtitleManager subtitleManager;
    public string[] subtitles;

    public void TriggerSubtitle(Action callback = null)
    {
        subtitleManager.StartSubtitle(subtitles, callback);
    }
}

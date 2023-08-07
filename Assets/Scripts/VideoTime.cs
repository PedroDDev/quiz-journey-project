using System;
using UnityEngine;
using UnityEngine.UI;

public class VideoTime : MonoBehaviour
{
    public VideoController videoController;
    public Text text;


    void Update()
    {
        TimeSpan duration = TimeSpan.FromSeconds(videoController.Duration);
        TimeSpan currentTime = TimeSpan.FromSeconds(videoController.Time);

        text.text = string.Format("{0,1:0}:{1,2:00}", currentTime.Minutes, currentTime.Seconds) + " / " + string.Format("{0,1:0}:{1,2:00}", duration.Minutes, duration.Seconds);
    }
}

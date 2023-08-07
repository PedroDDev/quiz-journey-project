using UnityEngine;

public class VideoPlayPause : MonoBehaviour
{
    public GameObject playButton;
    public GameObject pauseButton;

    public VideoController videoController;

    public void Play()
    {
        pauseButton.SetActive(true);
        playButton.SetActive(false);
    }

    public void Pause()
    {
        playButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    void Update()
    {
        if (videoController.IsDone)
        {
            pauseButton.SetActive(false);
            playButton.SetActive(true);
        }
    }
}

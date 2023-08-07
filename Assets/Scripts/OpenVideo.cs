using UnityEngine;

public class OpenVideo : MonoBehaviour
{
    public GameObject videoHUD;
    public GameObject showButton;
    public VideoController videoController;

    public void ShowVideo()
    {
        videoHUD.SetActive(true);
        showButton.SetActive(false);
        videoController.RestartVideo();
    }

    public void CloseVideo()
    {
        videoHUD.SetActive(false);
        showButton.SetActive(true);
        videoController.RestartVideo();
    }
}

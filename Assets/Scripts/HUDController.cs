using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    public GameObject goodEndHUD;
    public GameObject badEndHUD;

    public GameObject warningPanel;

    public void ShowGoodEndHUD()
    {
        goodEndHUD.SetActive(true);
        badEndHUD.SetActive(false);
    }

    public void ShowBadEndHUD()
    {
        badEndHUD.SetActive(true);
        goodEndHUD.SetActive(false);

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

    }

    public void ShowWarningPanel()
    {
        warningPanel.SetActive(true);
    }

    public void HideWarningPanel()
    {
        warningPanel.SetActive(false);
    }
}

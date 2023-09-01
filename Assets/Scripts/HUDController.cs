using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    [SerializeField] private GameObject goodEndHUD;
    [SerializeField] private GameObject badEndHUD;
    [SerializeField] private GameObject warningPanel;

    public void ShowGoodEndHUD()
    {
        goodEndHUD.SetActive(true);
    }

    public void HideGoodEndHUD()
    {
        goodEndHUD.SetActive(false);

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ShowBadEndHUD()
    {
        badEndHUD.SetActive(true);
    }

    public void HideBadEndHUD()
    {
        badEndHUD.SetActive(false);

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

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Start()
    {
        SceneManager.LoadScene("Demo");
    }
}

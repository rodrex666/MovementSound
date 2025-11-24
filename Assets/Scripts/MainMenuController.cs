using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene Names")]
    public string soloSceneName = "MainScene";

    [Header("Coming Soon Popup")]
    public AutoHidePopup popup;

    public void SoloMode()
    {
        SceneManager.LoadScene(soloSceneName);
    }

    public void MultiplayerMode()
    {
        popup.Show();    // <--- auto hides itself
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene Names")]
    public string soloSceneName = "MainScene";
    public string MultiplayerSceneName = "MultiplayerScene";

    [Header("Coming Soon Popup")]
    public AutoHidePopup popup;

    public void SoloMode()
    {
        SceneManager.LoadScene(soloSceneName);
    }

    public void MultiplayerMode()
    {
        SceneManager.LoadScene(MultiplayerSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

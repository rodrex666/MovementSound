using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenuGestureController : MonoBehaviour
{
    [Header("Hand References")]
    public OVRHand leftHand;
    public OVRHand rightHand;

    [Header("UI Menu")]
    public GameObject restartMenu;

    [Header("Settings")]
    public float gestureCooldown = 0.6f;  // shorter cooldown = more responsive
    private float lastGestureTime = 0f;

    private bool menuOpen = false;

    void Update()
    {
        if (leftHand == null || rightHand == null) return;

        if (IsPeaceSign(leftHand) && IsPeaceSign(rightHand))
        {
            TryToggleMenu();
        }
    }

    private void TryToggleMenu()
    {
        if (Time.time - lastGestureTime < gestureCooldown)
            return;

        lastGestureTime = Time.time;
        ToggleMenu();
    }

    private void ToggleMenu()
    {
        menuOpen = !menuOpen;
        restartMenu.SetActive(menuOpen);
    }

    public void Resume()
    {
        restartMenu.SetActive(false);
        menuOpen = false;
    }

    public void RestartExperience()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void QuitApp()
    {
        Application.Quit();
    }

    // ------------------------------
    // EASY peace sign detection
    // ------------------------------
    private bool IsPeaceSign(OVRHand hand)
    {
        if (!hand.IsTracked) return false;

        bool indexExtended =
            !hand.GetFingerIsPinching(OVRHand.HandFinger.Index) &&
            hand.GetFingerPinchStrength(OVRHand.HandFinger.Index) < 0.25f;

        bool middleExtended =
            !hand.GetFingerIsPinching(OVRHand.HandFinger.Middle) &&
            hand.GetFingerPinchStrength(OVRHand.HandFinger.Middle) < 0.25f;

        // Make curled fingers easier to detect
        bool ringCurled = hand.GetFingerPinchStrength(OVRHand.HandFinger.Ring) > 0.45f;
        bool pinkyCurled = hand.GetFingerPinchStrength(OVRHand.HandFinger.Pinky) > 0.45f;

        return indexExtended && middleExtended && ringCurled && pinkyCurled;
    }
}

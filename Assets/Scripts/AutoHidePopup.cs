using UnityEngine;

public class AutoHidePopup : MonoBehaviour
{
    [Header("Popup Root")]
    public GameObject popupRoot;

    [Header("Settings")]
    public float displayTime = 2.0f;   // how long it stays on screen

    private float timer = 0f;
    private bool showing = false;

    private void Update()
    {
        if (!showing) return;

        timer += Time.deltaTime;
        if (timer >= displayTime)
        {
            Hide();
        }
    }

    public void Show()
    {
        timer = 0f;
        showing = true;
        popupRoot.SetActive(true);
    }

    public void Hide()
    {
        showing = false;
        popupRoot.SetActive(false);
    }
}

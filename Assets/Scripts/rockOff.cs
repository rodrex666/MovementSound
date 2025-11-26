using System.Collections;
using UnityEngine;

public class rockOff : MonoBehaviour
{
    public float timeBeforeDestroy = 4f;
    void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeBeforeDestroy);
        Destroy(gameObject);
    }
}

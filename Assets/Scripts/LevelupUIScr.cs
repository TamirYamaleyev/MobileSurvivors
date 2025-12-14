using System.Collections;
using UnityEngine;

public class LevelUpUIScr : MonoBehaviour
{
    [SerializeField] float showDuration = 2f;
    Coroutine routine;
    public GameObject uiText;

    void Awake()
    {
        uiText.SetActive(false);
    }

    public void Show()
    {
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(ShowRoutine());
    }

    IEnumerator ShowRoutine()
    {
        uiText.SetActive(true);
        yield return new WaitForSeconds(showDuration);
        uiText.SetActive(false);
        routine = null;
    }
}

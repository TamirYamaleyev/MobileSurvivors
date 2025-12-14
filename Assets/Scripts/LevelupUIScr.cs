using System.Collections;
using UnityEngine;

public class LevelUpUIScr : MonoBehaviour
{
    [SerializeField] float showDuration = 2f;
    Coroutine routine;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(ShowRoutine());
    }

    IEnumerator ShowRoutine()
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(showDuration);
        gameObject.SetActive(false);
        routine = null;
    }
}

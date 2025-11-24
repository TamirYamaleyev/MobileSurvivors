using UnityEngine;
using TMPro;
using System.Collections;

public class HPHUD : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text hpText; // assign in inspector

    [Header("Settings")]
    public int maxHP = 100;
    public float countDuration = 0.25f;

    [Header("Flash Colors")]
    public Color damageFlashColor = Color.red;
    public Color healFlashColor = Color.green;
    public float flashDuration = 0.15f;

    private int currentHP;
    private Coroutine countRoutine;
    private Coroutine flashRoutine;
    private Color originalColor;

    void Start()
    {
        currentHP = maxHP;
        if (hpText != null)
            originalColor = hpText.color;

        UpdateHPInstant();
    }

    public void TakeDamage(int amount)
    {
        int newHP = Mathf.Clamp(currentHP - amount, 0, maxHP);
        UpdateHP(newHP, damageFlashColor);
    }

    public void Heal(int amount)
    {
        int newHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
        UpdateHP(newHP, healFlashColor);
    }

    private void UpdateHP(int newHP, Color flashColor)
    {
        // smooth counter
        if (countRoutine != null)
            StopCoroutine(countRoutine);
        countRoutine = StartCoroutine(SmoothCount(currentHP, newHP));

        // color flash
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);
        flashRoutine = StartCoroutine(ColorFlash(flashColor));

        currentHP = newHP;
    }

    private void UpdateHPInstant()
    {
        hpText.text = "HP: " + currentHP + "/" + maxHP;
    }

    private IEnumerator SmoothCount(int oldValue, int newValue)
    {
        float timer = 0f;

        while (timer < countDuration)
        {
            timer += Time.deltaTime;
            float t = timer / countDuration;

            int displayedValue = Mathf.RoundToInt(Mathf.Lerp(oldValue, newValue, t));
            hpText.text = $"HP: {displayedValue}/{maxHP}";

            yield return null;
        }

        hpText.text = $"HP: {newValue}/{maxHP}";
    }

    private IEnumerator ColorFlash(Color targetFlashColor)
    {
        float timer = 0f;

        // flash to target color
        while (timer < flashDuration)
        {
            timer += Time.deltaTime;
            float t = timer / flashDuration;
            hpText.color = Color.Lerp(originalColor, targetFlashColor, t);
            yield return null;
        }

        timer = 0f;

        // fade back to normal
        while (timer < flashDuration)
        {
            timer += Time.deltaTime;
            float t = timer / flashDuration;
            hpText.color = Color.Lerp(targetFlashColor, originalColor, t);
            yield return null;
        }

        hpText.color = originalColor;
    }
}

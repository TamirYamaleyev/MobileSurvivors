using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class BGMVolumeSlider : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer mixer;          // Assign your AudioMixer
    public string exposedParam;       // e.g., "BGMVolume" or "SFXVolume"

    [Header("UI Slider")]
    public Slider slider;             // The UI slider

    private const float minDb = -80f; // Minimum dB (silent)
    private const float maxDb = 0f;   // Maximum dB (full volume)

    void Start()
    {
        if (slider == null) slider = GetComponent<Slider>();

        // Ensure slider uses 0–1 range
        slider.minValue = 0f;
        slider.maxValue = 1f;

        // Load saved value if it exists
        float savedValue = PlayerPrefs.GetFloat(exposedParam, 1f);
        slider.value = savedValue;
        ApplyVolume(savedValue);

        // Listen for changes
        slider.onValueChanged.AddListener(ApplyVolume);
    }

    private void ApplyVolume(float value)
    {
        // Clamp to avoid log(0)
        float clampedValue = Mathf.Clamp(value, 0.0001f, 1f);

        // Map linear slider to logarithmic dB scale
        float dB = Mathf.Log10(clampedValue) * 20f;

        mixer.SetFloat(exposedParam, dB);

        // Save preference
        PlayerPrefs.SetFloat(exposedParam, value);
    }

    public void SetSliderValue(float value)
    {
        slider.value = value;
        ApplyVolume(value); // update the mixer
    }

}

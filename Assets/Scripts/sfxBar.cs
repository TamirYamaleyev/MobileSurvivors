using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValueDisplay : MonoBehaviour
{
    public Slider SfxVolume;       // The UI slider
    public TMP_Text VolumeValueSfx;  // Text that will display the value

    private void Start()
    {
        if (SfxVolume == null)
            SfxVolume = GetComponent<Slider>();

        UpdateValue(SfxVolume.value);

        // Register event for slider movement
        SfxVolume.onValueChanged.AddListener(UpdateValue);
    }

    private void UpdateValue(float value)
    {
        VolumeValueSfx.text = value.ToString("0"); // no decimals
        // For decimals: value.ToString("0.0")
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    public TextMeshProUGUI slotText;       // Shows "Empty" or saved name
    public Button saveButton;
    public Button loadButton;
    public int slotIndex;

    public void SetSlot(string saveName)
    {
        slotText.text = string.IsNullOrEmpty(saveName) ? "Empty" : saveName;
    }
}

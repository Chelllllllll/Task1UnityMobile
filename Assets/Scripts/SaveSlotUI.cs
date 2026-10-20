using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private RawImage thumbnail;
    [SerializeField] private TMP_Text label;

    private int slot;
    private Action<int> onClicked;

    public void Setup(int slotNumber, SaveData data, Texture2D screenshot, Action<int> clickCallback)
    {
        slot = slotNumber;
        onClicked = clickCallback;

        label.text = "Slot " + slot
                             + "\n" + data.SaveDateTime.ToString("dd.MM.yyyy HH:mm")
                             + "\nWeapon: " + data.WeaponId
                             + "\nScore: " + data.PlayerStats.Score;

        if (screenshot != null)
        {
            thumbnail.texture = screenshot;
        }

        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        if (onClicked != null)
        {
            onClicked(slot);
        }
    }
}
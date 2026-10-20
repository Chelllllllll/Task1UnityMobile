using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadMenu : MonoBehaviour
{
    [SerializeField] private GameObject loadPanel;
    [SerializeField] private Transform slotsContainer;
    [SerializeField] private SaveSlotUI slotPrefab;
    [SerializeField] private Button backButton;
    [SerializeField] private PauseMenu pauseMenu;

    private List<GameObject> spawnedSlots = new List<GameObject>();
    private List<Texture2D> loadedTextures = new List<Texture2D>();

    public bool IsOpen
    {
        get
        {
            return loadPanel.activeSelf;
        }
    }

    private void Start()
    {
        backButton.onClick.AddListener(OnBackClicked);
        loadPanel.SetActive(false);
    }

    public void Open()
    {
        loadPanel.SetActive(true);
        Refresh();
    }

    public void Close()
    {
        ClearSlots();
        loadPanel.SetActive(false);
    }

    private void Refresh()
    {
        ClearSlots();

        List<int> slots = SaveSystem.GetExistingSlots();

        for (int i = 0; i < slots.Count; i++)
        {
            int slot = slots[i];
            SaveData data = SaveSystem.Load(slot);

            if (data == null)
            {
                continue;
            }

            Texture2D screenshot = SaveSystem.LoadScreenshot(slot);

            if (screenshot != null)
            {
                loadedTextures.Add(screenshot);
            }

            SaveSlotUI slotUI = Instantiate(slotPrefab, slotsContainer);
            slotUI.Setup(slot, data, screenshot, OnSlotClicked);
            spawnedSlots.Add(slotUI.gameObject);
        }
    }

    private void ClearSlots()
    {
        for (int i = 0; i < spawnedSlots.Count; i++)
        {
            Destroy(spawnedSlots[i]);
        }

        spawnedSlots.Clear();

        for (int i = 0; i < loadedTextures.Count; i++)
        {
            Destroy(loadedTextures[i]);
        }

        loadedTextures.Clear();
    }

    private void OnSlotClicked(int slot)
    {
        bool loaded = GameManager.Instance.LoadFromSlot(slot);

        if (loaded)
        {
            Close();
            pauseMenu.Resume();
        }
    }

    private void OnBackClicked()
    {
        pauseMenu.ReturnFromLoadMenu();
    }
}
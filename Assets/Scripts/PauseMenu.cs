using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private SaveLoadMenu saveLoadMenu;
    [SerializeField] private Gun gun;
    [SerializeField] private FirstPersonLook look;

    private bool isPaused;
    private bool isSaving;

    private void Start()
    {
        saveButton.onClick.AddListener(OnSaveClicked);
        loadButton.onClick.AddListener(OnLoadClicked);
        resumeButton.onClick.AddListener(Resume);
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (isSaving)
        {
            return;
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!isPaused)
            {
                Pause();
            }
            else if (saveLoadMenu.IsOpen)
            {
                ReturnFromLoadMenu();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;

        gun.enabled = false;
        look.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

        gun.enabled = true;
        look.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ReturnFromLoadMenu()
    {
        saveLoadMenu.Close();
        pausePanel.SetActive(true);
    }

    private void OnLoadClicked()
    {
        pausePanel.SetActive(false);
        saveLoadMenu.Open();
    }

    private void OnSaveClicked()
    {
        if (isSaving)
        {
            return;
        }

        StartCoroutine(SaveRoutine());
    }

    private IEnumerator SaveRoutine()
    {
        isSaving = true;

        int slot = SaveSystem.GetNextFreeSlot();

        pausePanel.SetActive(false);
        yield return new WaitForEndOfFrame();

        Texture2D screenshot = ScreenCapture.CaptureScreenshotAsTexture();
        byte[] png = screenshot.EncodeToPNG();
        Destroy(screenshot);

        SaveSystem.SaveScreenshot(slot, png);
        GameManager.Instance.SaveToSlot(slot);

        Debug.Log("Saved slot " + slot + " to " + SaveSystem.SaveFolder);

        pausePanel.SetActive(true);
        isSaving = false;
    }
}
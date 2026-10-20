using UnityEngine;
using UnityEngine.InputSystem;

public class SaveTest : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current.f5Key.wasPressedThisFrame)
        {
            GameManager.Instance.SaveToSlot(1);
            Debug.Log("Saved: " + SaveSystem.GetJsonPath(1));
        }

        if (Keyboard.current.f9Key.wasPressedThisFrame)
        {
            int latestSlot = SaveSystem.GetNextFreeSlot() - 1;

            if (latestSlot < 1)
            {
                Debug.Log("No saves found");
                return;
            }

            bool loaded = GameManager.Instance.LoadFromSlot(latestSlot);
            Debug.Log("Loaded slot " + latestSlot + ": " + loaded);
        }
    }
}
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private const string SaveFolderName = "Saves";

    public static string SaveFolder
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, SaveFolderName);
        }
    }

    public static string GetJsonPath(int slot)
    {
        return Path.Combine(SaveFolder, $"slot_{slot}.json");
    }

    public static string GetScreenshotPath(int slot)
    {
        return Path.Combine(SaveFolder, $"slot_{slot}.png");
    }

    public static bool SlotExists(int slot)
    {
        return File.Exists(GetJsonPath(slot));
    }

    public static void Save(int slot, SaveData data)
    {
        Directory.CreateDirectory(SaveFolder);
        string json = NewtonsoftJsonPacker.Pack(data);
        File.WriteAllText(GetJsonPath(slot), json);
    }

    public static SaveData Load(int slot)
    {
        if (!SlotExists(slot))
        {
            return null;
        }

        try
        {
            string json = File.ReadAllText(GetJsonPath(slot));
            SaveData data = NewtonsoftJsonPacker.Unpack<SaveData>(json);

            if (data == null || data.PlayerStats == null)
            {
                return null;
            }

            return data;
        }
        catch (System.Exception exception)
        {
            Debug.LogWarning("Slot " + slot + " is corrupted: " + exception.Message);
            return null;
        }
    }

    public static void SaveScreenshot(int slot, byte[] png)
    {
        Directory.CreateDirectory(SaveFolder);
        File.WriteAllBytes(GetScreenshotPath(slot), png);
    }

    public static Texture2D LoadScreenshot(int slot)
    {
        string path = GetScreenshotPath(slot);

        if (!File.Exists(path))
        {
            return null;
        }

        byte[] png = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(png);
        return texture;
    }

    public static int GetNextFreeSlot()
    {
        int slot = 1;

        while (SlotExists(slot))
        {
            slot = slot + 1;
        }

        return slot;
    }

    public static List<int> GetExistingSlots()
    {
        List<int> slots = new List<int>();

        if (!Directory.Exists(SaveFolder))
        {
            return slots;
        }

        string[] files = Directory.GetFiles(SaveFolder, "slot_*.json");

        for (int i = 0; i < files.Length; i++)
        {
            string fileName = Path.GetFileNameWithoutExtension(files[i]);
            string numberPart = fileName.Substring("slot_".Length);

            int slot;
            if (int.TryParse(numberPart, out slot))
            {
                slots.Add(slot);
            }
        }

        slots.Sort();
        return slots;
    }
}
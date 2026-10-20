using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Gun gun;
    [SerializeField] private Rigidbody playerBody;
    [SerializeField] private FirstPersonLook look;
    [SerializeField] private Target[] targets;
    [SerializeField] private TMP_Text scoreText;

    private int health = 100;
    private int score = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score = score + amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public SaveData CreateSaveData()
    {
        SaveData data = new SaveData();

        data.PlayerStats = new PlayerStats();
        data.PlayerStats.Health = health;
        data.PlayerStats.Score = score;

        data.WeaponId = gun.GetCurrentWeaponId();

        data.PositionX = playerBody.position.x;
        data.PositionY = playerBody.position.y;
        data.PositionZ = playerBody.position.z;

        Vector2 angles = look.GetLookAngles();
        data.LookYaw = angles.x;
        data.LookPitch = angles.y;

        data.SaveDateTime = DateTime.Now;

        data.Targets = new List<TargetSaveData>();
        for (int i = 0; i < targets.Length; i++)
        {
            TargetSaveData targetData = new TargetSaveData();
            targetData.Health = targets[i].Health;
            targetData.IsAlive = targets[i].gameObject.activeSelf;
            data.Targets.Add(targetData);
        }

        return data;
    }

    public void ApplySaveData(SaveData data)
    {
        health = data.PlayerStats.Health;
        score = data.PlayerStats.Score;
        UpdateScoreText();

        gun.SetWeaponById(data.WeaponId);

        playerBody.position = new Vector3(data.PositionX, data.PositionY, data.PositionZ);
        playerBody.linearVelocity = Vector3.zero;

        look.SetLookAngles(new Vector2(data.LookYaw, data.LookPitch));

        if (data.Targets != null)
        {
            for (int i = 0; i < targets.Length && i < data.Targets.Count; i++)
            {
                targets[i].SetState(data.Targets[i].Health, data.Targets[i].IsAlive);
            }
        }
    }

    public void SaveToSlot(int slot)
    {
        SaveData data = CreateSaveData();
        SaveSystem.Save(slot, data);
    }

    public bool LoadFromSlot(int slot)
    {
        SaveData data = SaveSystem.Load(slot);

        if (data == null)
        {
            return false;
        }

        ApplySaveData(data);
        return true;
    }
}
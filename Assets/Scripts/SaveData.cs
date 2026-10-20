using UnityEngine;
using System.Collections.Generic;
using System;

public class SaveData
{
    public PlayerStats PlayerStats { get; set; }
    public string WeaponId { get; set; }
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float PositionZ { get; set; }
    public float LookYaw { get; set; }
    public float LookPitch { get; set; }
    public DateTime SaveDateTime { get; set; }
    public List<TargetSaveData> Targets { get; set; }
}

public class TargetSaveData
{
    public int Health { get; set; }
    public bool IsAlive { get; set; }
}


using System;
using UnityEngine;

[Serializable]

public struct UpgradeContext
{
    public string Name;
    public Sprite Icon;
    [Header("�̔����i")] public int Cost;
    [Header("����������{��")] public float Value;
}

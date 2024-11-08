using System;
using UnityEngine;

[Serializable]

public struct UpgradeContext
{
    public string Name;
    public Sprite Icon;
    [Header("販売価格")] public int Cost;
    [Header("増加させる倍率")] public float Mag;
}

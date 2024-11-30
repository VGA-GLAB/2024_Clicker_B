using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 施設のレベルごとの毎秒獲得できるリソース数やコストの情報を持つスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "FacilityUpgradeContext", menuName = "FacilityUpgradeContext")]
public class FacilityUpgradeContext : ScriptableObject
{
    public List<DataByLevel> UpgradeData = new List<DataByLevel>();

    /// <summary>
    ///  引数で渡されたレベルに対応したデータを返します
    /// </summary>
    public DataByLevel Upgrade(int level)
    {
        return UpgradeData[level];
    }
}

[System.Serializable]
public struct DataByLevel
{
    public string Level;
    [Header("毎秒獲得できるリソース数")] public int ResourcePerSecond;
    [Header("レベルアップに必要なリソース")] public int LevelUpCost;
}
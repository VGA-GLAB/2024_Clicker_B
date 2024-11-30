using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 施設のデータを管理するクラスです
/// </summary>
public class FacilityManager : MonoBehaviour
{
    [SerializeField] private FacilityUpgradeContext _facilityUpgradeContext;
    public List<FacilityData> _facilities;
    
    private void Start()
    {
        _facilities = new List<FacilityData>()
        {
            new FacilityData { Level = 1, ResourcePerSecond = 100, TotalResource = 0 },
            new FacilityData { Level = 1, ResourcePerSecond = 100, TotalResource = 0 },
            new FacilityData { Level = 1, ResourcePerSecond = 100, TotalResource = 0 },
            new FacilityData { Level = 1, ResourcePerSecond = 100, TotalResource = 0 }, 
            new FacilityData { Level = 1, ResourcePerSecond = 100, TotalResource = 0 },
        };
    }
    
    /// <summary>
    /// 施設のレベルアップを行う
    /// </summary>
    public void LevelUp(int facilityNum)
    {
        if (_facilities[facilityNum].Level <= 2) //レベルが2以下なら
        {
            _facilities[facilityNum].Level++;
            UpGrade(facilityNum);
        }
        else
        {
            //TODO: レベルが3のときの処理が必要ならここに書く
            Debug.Log("レベルが上限に達しています");
        }
    }

    /// <summary>
    /// レベルアップに合わせて施設の情報を書き換えます
    /// </summary>
    private void UpGrade(int facilityNum)
    {
        _facilities[facilityNum].ResourcePerSecond = _facilityUpgradeContext.Upgrade(_facilities[facilityNum].Level - 1).ResourcePerSecond;
    }
}

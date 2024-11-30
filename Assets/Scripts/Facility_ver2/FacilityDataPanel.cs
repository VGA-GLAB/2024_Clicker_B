using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 施設をクリックした時に開くパネルを管理するクラス
/// </summary>
public class FacilityDataPanel : MonoBehaviour
{
    [SerializeField] FacilityManager _facilityManager;
    [SerializeField, Header("施設の情報を確認するパネル")] private GameObject _dataPanel;
    [SerializeField] Text _level, _resourecePerSecond, _totlaResourece;
    private int _facilityNum;

    private void Start()
    {
        _dataPanel.SetActive(false);
    }

    /// <summary>
    /// パネルを開きます
    /// </summary>
    public void PanelOpen(int facilityNum)
    {
        //TODO: パネルの位置を、施設の近くに移動させる処理
        _dataPanel.SetActive(true);
        DataUpdate(facilityNum);
    }

    /// <summary>
    /// パネルを閉じます
    /// </summary>
    public void PanelClose()
    {
        _dataPanel.SetActive(false);
    }
    
    /// <summary>
    /// クリックした施設によって、パネルの情報を書き換える処理
    /// </summary>
    private void DataUpdate(int facilityNum)
    {
        _facilityNum = facilityNum - 1; 
        _level.text = _facilityManager._facilities[_facilityNum].Level.ToString();
        _resourecePerSecond.text = _facilityManager._facilities[_facilityNum].ResourcePerSecond.ToString();
    }
    
    private void DataUpdate()
    {
        _level.text = _facilityManager._facilities[_facilityNum].Level.ToString();
        _resourecePerSecond.text = _facilityManager._facilities[_facilityNum].ResourcePerSecond.ToString();
    }

    public void Upgrade()
    {
        _facilityManager.LevelUp(_facilityNum);
        DataUpdate();
    }
}

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 施設をクリックした時に開くパネルを管理するクラス
/// </summary>
public class FacilityDataPanel : MonoBehaviour
{
    [SerializeField] private FacilityManager _facilityManager;
    [SerializeField, Header("施設の情報を確認するパネル")] private GameObject _dataPanel;
    [SerializeField] private Text _level, _resourecePerSecond, _totlaResourece, _cost;
    [SerializeField] private Vector3[] _panelPos;
    private int _facilityNum;
    private bool[] _openFlg = new bool[5];
    

    private void Start()
    {
        _dataPanel.SetActive(false);
    }

    /// <summary>
    /// パネルを開きます
    /// </summary>
    public void PanelOpen(int facilityNum)
    {
        _facilityNum = facilityNum - 1;
        
        if (_openFlg[_facilityNum])
        {
            _openFlg[_facilityNum] = false;
            PanelClose();
        }
        else
        {
            _openFlg[_facilityNum] = true;
            _dataPanel.transform.localPosition = _panelPos[_facilityNum];
            _dataPanel.SetActive(true);
            DataUpdate();
        }
    }

    /// <summary>
    /// パネルを閉じます
    /// </summary>
    public void PanelClose()
    {
        _dataPanel.SetActive(false);
        _openFlg[_facilityNum] = false;
    }
    
    private void DataUpdate()
    {
        _level.text = $"現在のLv {_facilityManager._facilities[_facilityNum].Level}";
        _resourecePerSecond.text = $"毎秒{_facilityManager._facilities[_facilityNum].ResourcePerSecond.ToString()}リソース";
        _totlaResourece.text = $"今までの総取得リソース";
        _cost.text = $"コスト {_facilityManager._facilityUpgradeContext.Upgrade(_facilityManager._facilities[_facilityNum].Level - 1).LevelUpCost}";
    }

    public void Upgrade()
    {
        _facilityManager.LevelUp(_facilityNum);
        DataUpdate();
    }
}

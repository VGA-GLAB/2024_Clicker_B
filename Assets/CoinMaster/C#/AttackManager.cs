using CoinMaster;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _selectData;
    [SerializeField]  private Text[] _selectText;
    private ListData _listData;
    void Awake()
    {
        _selectText = _selectData.Select(x => x.GetComponentInChildren<Text>()).ToArray();

        var buttons = _selectData.Select(x => x.GetComponent<NewButton>()).ToArray();
    }
    public async void AttackNow()
    {
        foreach (var item in _selectData)
        {
            item.SetActive(false);
        }

        CoinManager.Instance._panelState = CoinManager.PanelState.Attack;
        CoinManager.Instance.PanelActive();

        _listData = await CoinMasterNetwork.GetList();

        int j = 0;
        for (int i = 0; i < _selectData.Length; i++,j++) 
        {
            if(SaveMachine.Instance.loginData.user.name == _listData.list[j].name)
            {
                j++;
            }
            _selectData[i].SetActive(j < _listData.list.Length);
            if(i >= _listData.list.Length)
                continue;
            _selectText[i].text = $"{_listData.list[j].name}  {_listData.list[j].coin}G";
        }

        //CoinMasterNetwork.Attack();
    }
}

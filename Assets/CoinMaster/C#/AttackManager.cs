using CoinMaster;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _selectData;
    [SerializeField] private Text[] _selectText;
    [SerializeField] private Text[] _selectText2;
    [SerializeField] private NewButton[] _selectButton;
    private ListData _listData;
    void Awake()
    {
        _selectText = _selectData.Select(x => x.GetComponentInChildren<Text>()).ToArray();
        _selectText2 = _selectText.Select(x => x.transform.GetComponentsInChildren<Text>()[1]).ToArray();

        _selectButton = _selectData.Select(x => x.GetComponent<NewButton>()).ToArray();
    }
    public async void AttackNow(long bet)
    {
        foreach (var item in _selectData)
        {
            item.SetActive(false);
        }

        CoinManager.Instance._panelState = CoinManager.PanelState.Attack;
        CoinManager.Instance.PanelActive();

        _listData = await CoinMasterNetwork.GetList();

        foreach (var item in _selectButton)
        {
            item.OnClick.RemoveAllListeners();
        }


        int y = 0;
        for (int x = 0; x < _selectData.Length; x++,y++) 
        {
            if(SaveMachine.Instance.loginData.user.uuid == _listData.list[y].uuid)
            {
                //y++;
            }
            _selectData[x].SetActive(y < _listData.list.Length);
            if(x >= _listData.list.Length)
                continue;
            _selectText[x].text = $"{_listData.list[y].name}";
            _selectText2[x].text = $"{_listData.list[y].coin.ToString("#,0")}G";

            string str = _listData.list[y].uuid;
            _selectButton[x].OnClick.AddListener(() => Attack(str, bet));
        }
    }
    private async void Attack(string userId,long bet = 0)
    {
        var result = await CoinMasterNetwork.Attack(userId, bet);
        CoinManager.Instance._panelState = CoinManager.PanelState.Slot;
        CoinManager.Instance.PanelActive();
        Debug.Log(JsonUtility.ToJson(result));
        CoinManager.Instance.AttackResult(result.stealCoin);
    }
}

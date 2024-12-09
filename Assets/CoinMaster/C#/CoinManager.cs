using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    [SerializeField] private SlotAnimeSystem _slotAnimeSystem;
    [SerializeField] private NewButton _coinButton;
    [SerializeField] private NewButton _slotButton;
    [SerializeField] private NewButton _atkButton;

    [SerializeField] private NewButton _slotOpenButton;

    [SerializeField] private Text _coinText;
    [SerializeField] private Text _slotCoinText;
    [SerializeField] private Building[] _buildings;

    [SerializeField] private GameObject _atkPanel;
    [SerializeField] private GameObject _slotPanel;
    public Coin coin { get; private set; }

    public Coin totalTakeCoin;

    private SlotSystem _slotSystem;
    private SlotSystem.ResultData _resultData;

    public int[] buildingsLv()
        => _buildings.Select(x => x.level).ToArray();

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < _buildings.Length; i++)
        {
            _buildings[i].Manager = this;
        }
    }   

    private void Start()
    {
        _atkPanel.SetActive(true);
        _slotPanel.SetActive(true);


        _slotSystem = new();
        
        _coinButton.OnClick.AddListener(AddCoin);
        _coinButton.OnClick.AddListener(ChangeCoinText);
        
        _atkButton.OnClick.AddListener(CloseAtkPanel);
        
        _slotButton.OnClick.AddListener(Slot);

        _slotOpenButton.OnClick.AddListener(OpenSlotPanel);
        
        _atkPanel.SetActive(false);
        _slotPanel.SetActive(false);

        var faci = SaveMachine.Decode(SaveMachine.Instance.saveData.Facility);
        _buildings[0].level = faci.a;
        _buildings[1].level = faci.b;
        _buildings[2].level = faci.c;
        _buildings[3].level = faci.d;
        _buildings[4].level = faci.e;
        foreach (var building in _buildings)
        {
            building.TextUpdate();
        }
        
        coin = new Coin(SaveMachine.Instance.saveData.Resource);
        totalTakeCoin = new Coin(SaveMachine.Instance.saveData.Resource);
    }

    private void Update()
    {
        double add = _buildings.Sum(x => x.CoinPerSec[x.level]);

        coin += add * Time.deltaTime;
        totalTakeCoin += add * Time.deltaTime;
        ChangeCoinText();
    }

    private void Slot()
    {
        _slotSystem.CalcSlot(out _resultData);
        _slotAnimeSystem.Slot(_resultData.resultEnum);

    }
    public void SlotResult(int bet)
    {
        if(_resultData.isBolt)
            _atkPanel.SetActive(true);
        coin += (bet * _resultData.times) - bet;
        totalTakeCoin += (bet * _resultData.times) - bet;
    }

    private void CloseAtkPanel()
    {
        _atkPanel.SetActive(false);
    }

    private void OpenSlotPanel()
    {
        _slotPanel.SetActive(true);
    }
    public void CloseSlotPanel()
    {
        _slotPanel.SetActive(false);
    }
    
    public bool CanBuy(double cost)
        => (coin >= cost);

    public void BuyBuilding(double cost)
    {
        coin -= cost;
    }

    private void AddCoin()
    {
        double add = _buildings.Sum(x => x.CoinPerClick[x.level]);

        coin += 1 + add;
        totalTakeCoin++;
    }
    private void ChangeCoinText()
    {
        _coinText.text = coin.ToString();
        _slotCoinText.text = coin.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public PanelState _panelState;
    public enum PanelState
    {
        InGame,
        Village,
        Slot,
        Attack,
        NowLogin,
        NameSetting,
    }

    [Header("InGame")]
    [SerializeField] private NewButton _coinButton;
    [Space]
    [SerializeField] private NewButton _slotOpenButton;
    [SerializeField] private NewButton _villageOpenButton;
    [Space]
    [SerializeField] private Text _coinText;
    [Space]
    [SerializeField] private Text _resourceStatusText;
    [SerializeField] private Text _buildingStatusLeftText;
    [SerializeField] private Text _buildingStatusRightText;
    [Space]
    [SerializeField] private GameObject _ingamePanel;

    [Header("Slot")]
    [SerializeField] private SlotAnimeSystem _slotAnimeSystem;
    [SerializeField] private Text _slotCoinText;
    [Space]
    [SerializeField] private NewButton _slotButton;
    [Space]
    [SerializeField] private GameObject _slotPanel;

    [Header("Village")]
    [SerializeField] private Building[] _buildings;
    [SerializeField] private NewButton _villageCloseButton;
    [Space]
    [SerializeField] private GameObject _villagePanel;

    [Header("Attack")]
    [SerializeField] private AttackManager _attackManager;
    [SerializeField] private NewButton _atkButton;
    [Space]
    [SerializeField] private GameObject _atkPanel;

    [Header("NameSetting")]
    [SerializeField] private NameSettingManager _nameManager;
    [Space]
    [SerializeField] private GameObject _nameSettingPanel;

    [Header("Login")]
    [SerializeField] private GameObject _loginPanel;
    public Building[] Buildings => _buildings;


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
        _villagePanel.SetActive(true);
        _ingamePanel.SetActive(true);
        _nameSettingPanel.SetActive(true);
        _loginPanel.SetActive(true);


        _slotSystem = new();
        
        _coinButton.OnClick.AddListener(AddCoin);
        _coinButton.OnClick.AddListener(ChangeCoinText);
        
        _atkButton.OnClick.AddListener(() => _panelState = PanelState.Slot);
        _atkButton.OnClick.AddListener(PanelActive);

        _slotOpenButton.OnClick.AddListener(() => _panelState = PanelState.Slot);
        _slotOpenButton.OnClick.AddListener(PanelActive);

        _villageOpenButton.OnClick.AddListener(() => _panelState = PanelState.Village);
        _villageOpenButton.OnClick.AddListener(PanelActive);

        _villageCloseButton.OnClick.AddListener(() => _panelState = PanelState.InGame);
        _villageCloseButton.OnClick.AddListener(PanelActive);


        _slotButton.OnClick.AddListener(Slot);

        


        _atkPanel.SetActive(false);
        _slotPanel.SetActive(false);
        _villagePanel.SetActive(false);
        _nameSettingPanel.SetActive(false);
        _loginPanel.SetActive(false);

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

        _panelState = PanelState.NowLogin;
        PanelActive();
    }

    private void Update()
    {
        _resourceStatusText.text = $"{1 + (long)_buildings.Sum(x => x.CoinPerClick[x.level])}coin –ˆƒNƒŠƒbƒN\n{(long)_buildings.Sum(x => x.CoinPerSec[x.level])}coin –ˆ•b";
        _buildingStatusLeftText.text = string.Join("\n", _buildings.Select(x => x.Name).ToArray());

        _buildingStatusRightText.text = string.Join("\n", _buildings.Select(y => $": Level "+ ((y.level is 3) ? "Max" : y.level)).ToArray());


        double add = _buildings.Sum(x => x.CoinPerSec[x.level]);

        coin += add * Time.deltaTime;
        totalTakeCoin += add * Time.deltaTime;
        ChangeCoinText();

        if (Input.GetKey(KeyCode.Backspace) && Input.GetKey(KeyCode.PageDown) && Input.GetKey(KeyCode.Minus))
            coin += 100000000000;
    }

    private void Slot()
    {
        _slotSystem.CalcSlot(out _resultData);
        _slotAnimeSystem.Slot(_resultData.resultEnum);

    }
    public void SlotResult(int bet)
    {
        if(_resultData.isBolt)
        {
            _attackManager.AttackNow();
            return;
        }
        coin += (bet * _resultData.times) - bet;
        totalTakeCoin += (bet * _resultData.times) - bet;
    }
    [ContextMenu("‹­§UŒ‚")]
    void Attack()
    {
        _attackManager.AttackNow();
    }
    public void PanelActive()
    {
        _atkPanel.SetActive(_panelState is PanelState.Attack);
        _ingamePanel.SetActive(_panelState is PanelState.InGame);
        _slotPanel.SetActive(_panelState is PanelState.Slot);
        _villagePanel.SetActive(_panelState is PanelState.Village);
        _nameSettingPanel.SetActive(_panelState is PanelState.NameSetting);
        _loginPanel.SetActive(_panelState is PanelState.NowLogin);
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

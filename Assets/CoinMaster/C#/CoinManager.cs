using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private NewButton _coinButton;
    [SerializeField] private NewButton _slotButton;
    [SerializeField] private NewButton _atkButton;
    [SerializeField] private Text _coinText;
    [SerializeField] private Building[] _buildings;

    [SerializeField] private GameObject _atkPanel;
    
    private Coin coin;

    private void Awake()
    {
        for (int i = 0; i < _buildings.Length; i++)
        {
            _buildings[i].Manager = this;
        }
    }   

    private void Start()
    {
        _coinButton.OnClick.AddListener(AddCoin);
        _coinButton.OnClick.AddListener(ChangeCoinText);
        
        _atkButton.OnClick.AddListener(CloseAtkPanel);
        
        _slotButton.OnClick.AddListener(Slot);
        
        _atkPanel.SetActive(false);
        
        coin = new Coin(0);
    }

    private void Update()
    {
        double add = 0;
        for (int i = 0; i < _buildings.Length; i++)
            add += _buildings[i].CoinPerSec[_buildings[i].level];

        coin += add * Time.deltaTime;
        ChangeCoinText();
    }

    private void Slot()
    {
        bool correct = Random.Range(0, 10) == 0;
        if(!correct)
            return;
        float times = Random.Range(2, 11);
        if(times == 10)
            _atkPanel.SetActive(true);
        coin *= times;
    }

    private void CloseAtkPanel()
    {
        _atkPanel.SetActive(false);
    }
    
    public bool CanBuy(double cost)
        => (coin >= cost);

    public void BuyBuilding(double cost)
    {
        coin -= cost;
    }

    private void AddCoin()
    {
        coin++;
    }
    private void ChangeCoinText()
    {
        _coinText.text = coin.ToString();
    }
}

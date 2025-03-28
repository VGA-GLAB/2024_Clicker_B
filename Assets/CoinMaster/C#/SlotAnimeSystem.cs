using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SlotAnimeSystem : MonoBehaviour
{
    [SerializeField] private NewButton _slotButton;
    [Space]
    [SerializeField]
    private RectTransform _rollRectLeft;
    [SerializeField]
    private RectTransform _rollRectMiddle;
    [SerializeField]
    private RectTransform _rollRectRight;
    [Space]
    [SerializeField]
    private NewButton _xButton;

    [Space]
    [SerializeField]
    private NewButton _betUpButton;
    [SerializeField]
    private NewButton _betDownButton;
    [SerializeField]
    private Text _betText;

    [Space]
    [SerializeField]
    private GameObject _lowMoneyObj;

    private bool _isNowRoll;
    public bool IsSpinning { get { return _isNowRoll; } }
    private long _betValue;

    bool isBetUp;
    bool isBetDown;
    float betStartTime;

    [SerializeField]
    SlotSystem.ResultEnum _resultEnum;

    void Start()
    {
        _rollRectLeft.anchoredPosition = new Vector2(-250, Random.Range(0, 5) * 300 % 2100);
        _rollRectMiddle.anchoredPosition = new Vector2(0, Random.Range(6, 9) * 300 % 2100);
        _rollRectRight.anchoredPosition = new Vector2(250, Random.Range(9, 16) * 300 % 2100);

        _xButton.OnClick.AddListener(CloseSlotPanel);

        _betUpButton.OnClick.AddListener(() => BetUp(1));
        _betUpButton.OnClick.AddListener(() => betStartTime = Time.time);
        _betUpButton.OnClicked.AddListener(BetUpped);

        _betDownButton.OnClick.AddListener(() => BetDown(1));
        _betDownButton.OnClick.AddListener(() => betStartTime = Time.time);
        _betDownButton.OnClicked.AddListener(BetDowned);

        _betValue = 1;

        _slotButton.OnClick.AddListener(() => {if (!_isNowRoll) CoinManager.Instance.Slot(); });
        BetTextUpdate();
    }
    [ContextMenu("Start")]
    public void Slot()
    {
        Slot(_resultEnum);
    }
    public void Slot(SlotSystem.ResultEnum result = SlotSystem.ResultEnum.Miss)
    {
        if (CoinManager.Instance.coin.ToLong() <= 0)
            return;
        if (CoinManager.Instance.coin.ToLong() < _betValue)
            return;
        if (_isNowRoll)
            return;

        _betValue = BetClamp(_betValue);

        _isNowRoll = true;
        _rollRectLeft.anchoredPosition = new Vector2(-250, Random.Range(0, 100) * 300 % 2100);
        _rollRectMiddle.anchoredPosition = new Vector2(0, Random.Range(0, 100) * 300 % 2100);
        _rollRectRight.anchoredPosition = new Vector2(250, Random.Range(0, 100) * 300 % 2100);

        StartCoroutine(RollAnime(result));
    }
    void Update()
    {
        _lowMoneyObj.SetActive(CoinManager.Instance.coin.ToLong() < _betValue);

        if (Time.time - betStartTime < 0.5f)
            return;

        long value = (Time.time - betStartTime) switch
        {
            < 2 => 1,
            < 5 => 10,
            < 8 => 100,
            < 11 => (long)1e3,
            < 14 => (long)1e4,
            < 17 => (long)1e5,
            < 20 => (long)1e6,
            < 23 => (long)1e7,
            < 26 => (int)1e8,
            < 29 => (int)1e9,
            < 32 => (long)1e10,
            < 35 => (long)1e11,
            < 38 => (long)1e12,
            < 41 => (long)1e13,
            < 44 => (long)1e14,
            < 47 => (long)1e15,
            _ => (long)1e16,
        };

        if(isBetUp)
            BetUp(value);
        if(isBetDown)
            BetDown(value);
    }
    IEnumerator RollAnime(SlotSystem.ResultEnum mode)
    {
        float left = _rollRectLeft.anchoredPosition.y;
        float middle = _rollRectMiddle.anchoredPosition.y;
        float right = _rollRectRight.anchoredPosition.y;
        int stop = 0;

        int seed = Random.Range(0, 999);
        int a = Random.Range(0, 2);

        float startTime = Time.time;
        while (true)
        {
            left = CalcRoll(left, mode, ref stop, 1, seed);
            middle = CalcRoll(middle, mode, ref stop, 3, seed + a);
            right = CalcRoll(right, mode, ref stop, 5, seed + 5);

            left %= 2400;
            middle %= 2400;
            right %= 2400;

            _rollRectLeft.anchoredPosition = new Vector2(-250, left);
            _rollRectMiddle.anchoredPosition = new Vector2(0, middle);
            _rollRectRight.anchoredPosition = new Vector2(250, right);
            yield return null;

            if (stop is 0)
            {
                if (Time.time - startTime > 2)
                    stop = 1;
            }
            else if (stop is 6)
                break;
            else if (stop is 2 || stop is 4)
            {
                if (Time.time - startTime > 1)
                    stop++;
            }
            else
            {
                startTime = Time.time;
            }
        }
        _isNowRoll = false;
        Sloted();
    }
    void Sloted()
    {
        CoinManager.Instance.SlotResult(_betValue);
    }
    float CalcRoll(float current, SlotSystem.ResultEnum mode, ref int stop, int whenStop, int seed)
    {
        if (stop >= whenStop)
        {
            if (Mathf.Abs(SlotImgPos(mode, seed) - current) < 300)
            {
                stop = stop == whenStop ? whenStop + 1 : stop;
                return SlotImgPos(mode, seed);
            }
            else
                return current + 6000 * Time.deltaTime;
        }
        else
        {
            return current + 6000 * Time.deltaTime;
        }
    }
    int SlotImgPos(SlotSystem.ResultEnum mode, int missValue)
    {
        return mode switch
        {
            SlotSystem.ResultEnum.Coin => 0,
            SlotSystem.ResultEnum.Meat => 1,
            SlotSystem.ResultEnum.CoinBag => 2,
            SlotSystem.ResultEnum.Bank => 3,
            SlotSystem.ResultEnum.Box => 4,
            SlotSystem.ResultEnum.Bolt => 5,
            SlotSystem.ResultEnum.Trophy => 6,
            SlotSystem.ResultEnum.Clover => 7,
            SlotSystem.ResultEnum.Miss => missValue % 8,
            _ => missValue % 8,
        } * 300;
    }
    void BetTextUpdate()
    {
        _betText.text = $"BET: {_betValue.ToString("#,0")}";
    }
    void BetUp(long bet)
    {
        if (_isNowRoll)
            return;
        _betValue = BetClamp(_betValue + bet);
        isBetUp = true;
        isBetDown = false;
        BetTextUpdate();
    }
    void BetUpped()
    {
        isBetUp = false;
    }
    void BetDown(long bet)
    {
        if (_isNowRoll)
            return;
        _betValue = BetClamp(_betValue - bet);
        isBetDown = true;
        isBetUp = false;
        BetTextUpdate();
    }
    void BetDowned()
    {
        isBetDown = false;
    }
    void CloseSlotPanel()
    {
        if (_isNowRoll)
            return;
        CoinManager.Instance._panelState = CoinManager.PanelState.InGame;
        CoinManager.Instance.PanelActive();
    }
    long BetClamp(long bet)
        => bet >= (long)1e17 ? (long)1e17 : bet <= 1 ? 1 : bet;
}

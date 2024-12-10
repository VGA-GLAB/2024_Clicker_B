using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SlotAnimeSystem : MonoBehaviour
{
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

    private bool _isNowRoll;
    private int _betValue;

    [SerializeField]
    SlotSystem.ResultEnum _resultEnum;

    void Start()
    {
        _rollRectLeft.anchoredPosition = new Vector2(-250, Random.Range(0, 5) * 300 % 2100);
        _rollRectMiddle.anchoredPosition = new Vector2(0, Random.Range(6, 9) * 300 % 2100);
        _rollRectRight.anchoredPosition = new Vector2(250, Random.Range(9, 16) * 300 % 2100);

        _xButton.OnClick.AddListener(CloseSlotPanel);

        _betUpButton.OnClick.AddListener(BetUp);
        _betDownButton.OnClick.AddListener(BetDown);
        _betUpButton.OnClick.AddListener(BetTextUpdate);
        _betDownButton.OnClick.AddListener(BetTextUpdate);

        _betValue = 1;
        BetTextUpdate();
    }
    [ContextMenu("Start")]
    public void Slot()
    {
        Slot(_resultEnum);
    }
    public void Slot(SlotSystem.ResultEnum result = SlotSystem.ResultEnum.Miss)
    {
        if (_isNowRoll)
            return;
        _isNowRoll = true;
        _rollRectLeft.anchoredPosition = new Vector2(-250, Random.Range(0, 100) * 300 % 2100);
        _rollRectMiddle.anchoredPosition = new Vector2(0, Random.Range(0, 100) * 300 % 2100);
        _rollRectRight.anchoredPosition = new Vector2(250, Random.Range(0, 100) * 300 % 2100);

        StartCoroutine(RollAnime(result));
    }
    void Update()
    {
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

            if(stop is 0)
            {
                if (Time.time - startTime > 2)
                    stop = 1;
            }
            else if (stop is 6)
                break;
            else if (stop is 2||stop is 4)
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
    float CalcRoll(float current, SlotSystem.ResultEnum mode,ref int stop,int whenStop,int seed)
    {
        if (stop >= whenStop)
        {
            if (Mathf.Abs(SlotImgPos(mode,seed) - current) < 100)
            {
                stop = stop == whenStop ? whenStop + 1 : stop;
                return SlotImgPos(mode, seed);
            }
            else
                return current + 3000 * Time.deltaTime;
        }
        else
        {
            return current + 3000 * Time.deltaTime;
        }
    }
    int SlotImgPos(SlotSystem.ResultEnum mode,int missValue)
    {
        return mode switch
        {
            SlotSystem.ResultEnum.Coin => 0,
            SlotSystem.ResultEnum.Meat => 1,
            SlotSystem.ResultEnum.CoinBag => 2,
            SlotSystem.ResultEnum.Bank => 3,
            SlotSystem.ResultEnum.Box => 4,
            SlotSystem.ResultEnum.Bolt => 5,
            SlotSystem.ResultEnum.Clover => 6,
            SlotSystem.ResultEnum.Trophy => 7,
            SlotSystem.ResultEnum.Miss => missValue % 8,
            _ => missValue % 8,
        } * 300;
    }
    void BetTextUpdate()
    {
        _betText.text = $"BET: {_betValue}";
    }
    void BetUp()
    {
        if (!_isNowRoll)
            _betValue = Mathf.Clamp(_betValue + 1, 1, (int)CoinManager.Instance.coin.ToLong());
    }
    void BetDown()
    {
        if (!_isNowRoll)
            _betValue = Mathf.Clamp(_betValue - 1, 1, (int)CoinManager.Instance.coin.ToLong());
    }
    void CloseSlotPanel()
    {
        if(!_isNowRoll)
            CoinManager.Instance.CloseSlotPanel();
    }
}

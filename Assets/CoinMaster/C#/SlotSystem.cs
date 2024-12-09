using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSystem
{
    public struct ResultData
    {
        public int times;
        public bool isBolt;
        public ResultEnum resultEnum;
    }
    public enum ResultEnum
    {
        Bolt,
        Box,
        Trophy,
        Bank,
        CoinBag,
        Coin,
        Meat,
        Clover,
        Miss,
    }
    public void CalcSlot(out ResultData result)
    {


        float dice = Random.Range(0f, 99f);
        var resultEnum = dice switch
        {
            < (5 + 0) => ResultEnum.Bolt,
            < (0.1f + 5) => ResultEnum.Box,
            < (0.5f + 5.1f) => ResultEnum.Trophy,
            < (1.4f + 5.6f) => ResultEnum.Bank,
            < (3 + 7) => ResultEnum.CoinBag,
            < (5 + 10) => ResultEnum.Coin,
            < (10 + 15) => ResultEnum.Meat,
            < (30 + 25) => ResultEnum.Clover,
            _ => ResultEnum.Miss,
        };

        result.isBolt = false;
        result.times = 0;
        result.resultEnum = resultEnum;
        switch (resultEnum)
        {
            case ResultEnum.Bolt:
                result.isBolt = true;
                result.times = 10;
                break;
            case ResultEnum.Box:
                result.times = (int)1e4;
                break;
            case ResultEnum.Trophy:
                result.times = (int)1e3;
                break;
            case ResultEnum.Bank:
                result.times = (int)5e2;
                break;
            case ResultEnum.CoinBag:
                result.times = (int)2e2;
                break;
            case ResultEnum.Coin:
                result.times = (int)1e2;
                break;
            case ResultEnum.Meat:
                result.times = 10;
                break;
            case ResultEnum.Clover:
                result.times = 1;
                break;
            case ResultEnum.Miss:
            default:
                result.times = 0;
                break;
        }
    }
}

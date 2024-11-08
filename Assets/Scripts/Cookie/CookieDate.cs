using UnityEngine;

/// <summary>
/// クッキーのデータを管理しています
/// </summary>
public class CookieDate
{
    private float _cookies = 0; //クッキーの枚数
    private float _increaseCookie = 1; //1クリックで入手できるクッキーの枚数

    public float Cookies
    {
        get { return _cookies; }
        set { _cookies = value; }
    }
    public float IncreaseCookie
    {
        get { return _increaseCookie; }
    }

    /// <summary>
    /// 1クリックで入手できるクッキーの枚数を増やす
    /// </summary>
    /// <param name="mag">増加倍率</param>
    public void Increase(float mag)
    {
        _increaseCookie += _increaseCookie * mag;
        Debug.Log(_increaseCookie);
    }
}

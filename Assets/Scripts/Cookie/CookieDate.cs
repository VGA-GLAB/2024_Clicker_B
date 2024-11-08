using UnityEngine;

/// <summary>
/// クッキーのデータを管理しています
/// </summary>
public class CookieDate
{
    private float _cookies = 0; //クッキーの枚数
    private float _increaseCookie = 1.0f; //1クリックで入手できるクッキーの枚数

    public float Cookies
    {
        get { return _cookies; }
        set { _cookies = value; }
    }
    public float IncreaseCookie
    {
        get { return _increaseCookie; }
        set { _increaseCookie = value; }
    }
}

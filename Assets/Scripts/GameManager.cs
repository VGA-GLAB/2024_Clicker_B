using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GameManager : MonoBehaviour
{
    private UpgradeInstantiate _upgradeInst;

    /// <summary>クッキーの所持枚数</summary>
    public double Cookies {  get; set; }

    /// <summary>1クリックで入手できるクッキーの枚数</summary>
    public float IncreaseCookie { get; set; }

    /// <summary>1秒間に入手できるクッキーの枚数</summary>
    public float Cps { get; set; }
    
    private void Start()
    {
        Cookies = 0;
        IncreaseCookie = 1;
        Cps = 0;
    }

    private void Update()
    {
        // ToDo : 施設のCookie増加の処理を書く
    }

    /// <summary>
    /// クッキーをクリックしたら、所持クッキー枚数が増える
    /// </summary>
    public void CookieClicked()
    {
        var addCookie = Math.Ceiling(IncreaseCookie * 0.0001f);
        Cookies += addCookie;
    }
}

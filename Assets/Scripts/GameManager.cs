using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UpgradeInstantiate _upgradeInst;
    

    /// <summary>クッキーの所持枚数</summary>
    public double Cookies {  get; set; }

    /// <summary>1クリックで入手できるクッキーの枚数</summary>
    public float IncreaseCookie { get; set; }

    private void Start()
    {
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
        Cookies += IncreaseCookie;
    }

    
}

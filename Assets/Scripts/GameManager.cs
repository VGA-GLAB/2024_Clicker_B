using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject _upgradeButton;
    [SerializeField] private Transform _canvas;

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
        // ToDo : アップグレードのアイコンを表示する条件を書く


    }

    /// <summary>
    /// クッキーをクリックしたら、所持クッキー枚数が増える
    /// </summary>
    public void CookieClicked()
    {
        Cookies += IncreaseCookie;
    }

    /// <summary>
    /// アップグレードのアイコンをショップに表示する
    /// </summary>
    public void UpgradeUIInst(int id, int index)
    {
        GameObject upgradeIcon = Instantiate(_upgradeButton, _canvas); //アイコンを生成する
        UpgradeBase upgradeBase = upgradeIcon.GetComponent<UpgradeBase>();
        upgradeBase.IconChange(id, index);
    }
}

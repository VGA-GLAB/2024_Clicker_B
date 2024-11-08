using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject _upgradeButton;
    [SerializeField] private Transform _canvas;
    private CookieDate _cookieDate;

    private void Start()
    {
        _cookieDate = new CookieDate();
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
        _cookieDate.Cookies += _cookieDate.IncreaseCookie;
    }

    /// <summary>
    /// アップグレードのアイコンをショップに表示する
    /// </summary>
    public void UpgradeUIInst()
    {
        int id = 0;
        int index = 1;
        Instantiate(_upgradeButton, _canvas); //アイコンを生成する
        UpgradeBase upgradeBase = _upgradeButton.GetComponent<UpgradeBase>();
        upgradeBase.IconChange(id, index);
    }
}

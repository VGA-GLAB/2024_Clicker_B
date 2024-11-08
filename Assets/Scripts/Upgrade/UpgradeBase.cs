using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アップグレードの基盤のクラスです
/// </summary>
public class UpgradeBase : MonoBehaviour
{
    [SerializeField, Header("価格")] private Text _costText;
    [SerializeField, Header("アップグレードのアイコン")] private Image _icon;
    [SerializeField] private UpgradeDateBase _upgradeDB;
    private CookieDate _cookieDate;
    public int _cost;
    public float _value; //増加倍率
    private UpgradeEnum _upgrade;

    public int Cost
    {
        get { return _cost; }
    }

    private void Start()
    {
        _cookieDate = new CookieDate();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(Upgrade);
    }

    ///////////////生成時の機能///////////////

    /// <summary>
    /// ボタンの見た目を整えます
    /// </summary>
    public void IconChange(int id, int index)
    {
        List<UpgradeContext> list = _upgradeDB.GetList(id);
        _icon.sprite = list[index].Icon;
        _costText.text = list[index].Cost.ToString();
        _cost = list[index].Cost;
        _value = list[index].Value;
        _upgrade = id switch
        {
            0 => UpgradeEnum.Cookie,
            1 => UpgradeEnum.Cursor,
            2 => UpgradeEnum.Grandma,
            _ => UpgradeEnum.Cookie
        };
    }


    ///////////////ボタンの機能///////////////
    /// <summary>
    /// 倍率を変更する機能
    /// </summary>
    public void Upgrade()
    {
        float value = _upgrade switch
        {
            UpgradeEnum.Cookie => _cookieDate.IncreaseCookie,
            //UpgradeEnum.Cursor => カーソルが１秒ごとに入手できるクッキー枚数の変数
            //UpgradeEnum.Grandma => グランマが１秒ごとに入手できるクッキー枚数の変数 
            _ => 0
        };

        Debug.Log(value);
        value += value * (1f + _value);
        Debug.Log(_cookieDate.IncreaseCookie);
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// アップグレードの基盤のクラスです
/// </summary>
public class UpgradeBase : MonoBehaviour
{
    [SerializeField, Header("価格")] private Text _costText;
    [SerializeField, Header("アップグレードのアイコン")] private Image _icon;
    [SerializeField] private UpgradeDateBase _upgradeDB;
    private int _cost;
    private float _mag; //増加倍率
    private UpgradeEnum _upgrade;
    private GameManager _gameManager;

    public int Cost
    {
        get { return _cost; }
    }

    private void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>().GetComponent<GameManager>();
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
        _mag = list[index].Mag;
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
        switch (_upgrade)
        {
            case UpgradeEnum.Cookie:
                _gameManager.IncreaseCookie += _gameManager.IncreaseCookie * (1f + _mag);
                break;
            case UpgradeEnum.Cursor:
                break;
            case UpgradeEnum.Grandma:
                break;
        }
    }
}

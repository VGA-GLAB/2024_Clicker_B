using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �A�b�v�O���[�h�̊�Ղ̃N���X�ł�
/// </summary>
public class UpgradeBase : MonoBehaviour
{
    [SerializeField, Header("���i")] private Text _costText;
    [SerializeField, Header("�A�b�v�O���[�h�̃A�C�R��")] private Image _icon;
    [SerializeField] private UpgradeDateBase _upgradeDB;
    private CookieDate _cookieDate;
    public int _cost;
    public float _value; //�����{��
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

    ///////////////�������̋@�\///////////////

    /// <summary>
    /// �{�^���̌����ڂ𐮂��܂�
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


    ///////////////�{�^���̋@�\///////////////
    /// <summary>
    /// �{����ύX����@�\
    /// </summary>
    public void Upgrade()
    {
        float value = _upgrade switch
        {
            UpgradeEnum.Cookie => _cookieDate.IncreaseCookie,
            //UpgradeEnum.Cursor => �J�[�\�����P�b���Ƃɓ���ł���N�b�L�[�����̕ϐ�
            //UpgradeEnum.Grandma => �O�����}���P�b���Ƃɓ���ł���N�b�L�[�����̕ϐ� 
            _ => 0
        };

        Debug.Log(value);
        value += value * (1f + _value);
        Debug.Log(_cookieDate.IncreaseCookie);
    }
}

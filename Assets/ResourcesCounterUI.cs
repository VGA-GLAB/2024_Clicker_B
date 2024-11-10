using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ResourcesCounterUI : MonoBehaviour
{
    public static ResourcesCounterUI instance;
    private GameManager _gameManager;
    private Text _text;
    [SerializeField] private string _unitName;

    [SerializeField] private long _add;
    private void Start()
    {
        instance = this;
        _gameManager = FindAnyObjectByType<GameManager>();
        _text = GetComponent<Text>();
        UpdateText();
    }
    [ContextMenu("Click")]
    private void Click()
    {
        /*var value = _gameManager.IncreaseCookie;
        value = value <= 0 ? 1 : value;
        _gameManager.IncreaseCookie = _add;
            _gameManager.CookieClicked();
        _gameManager.IncreaseCookie = value;*/
    }
    public void UpdateText()
    {
        var value = _gameManager.Cookies;
        _text.text = $"{value.ToString("#,0")}{_unitName}";
    }
}

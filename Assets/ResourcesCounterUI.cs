using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesCounterUI : MonoBehaviour
{
    private GameManager _gameManager;
    private Text _text;
    [SerializeField] private string unitName;

    [SerializeField] private long add;
    private void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        _text = GetComponent<Text>();
    }
    [ContextMenu("Click")]
    private void Click()
    {
        var value = _gameManager.IncreaseCookie;
        value = value <= 0 ? 1 : value;
        _gameManager.IncreaseCookie = add;
            _gameManager.CookieClicked();
        _gameManager.IncreaseCookie = value;
    }
    private void Update()
    {
        var value = (BigInteger)_gameManager.Cookies;
        _text.text = $"{value.ToString("#,0")}{unitName}";
    }
}

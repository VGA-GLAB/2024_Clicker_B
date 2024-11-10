using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesCounterUI : MonoBehaviour
{
    private GameManager gameManager;
    private Text _text;
    [SerializeField] private string _unitName;

    [SerializeField] long Add;
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        _text = GetComponent<Text>();
        //v.Cookies
    }
    [ContextMenu("Click")]
    void Click()
    {
        var value = gameManager.IncreaseCookie;
        value = value <= 0 ? 1 : value;
        gameManager.IncreaseCookie = Add;
            gameManager.CookieClicked();
        gameManager.IncreaseCookie = value;
    }
    void Update()
    {
        var value = (BigInteger)gameManager.Cookies;
        _text.text = $"{value.ToString("#,0")}{_unitName}";
    }
}

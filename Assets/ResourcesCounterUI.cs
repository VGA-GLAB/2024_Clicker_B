using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesCounterUI : MonoBehaviour
{
    private GameManager gameManager;
    private Text _text;
    [SerializeField] private string _unitName;

    [SerializeField] int Add;
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        _text = GetComponent<Text>();
        //v.Cookies
    }
    [ContextMenu("Click")]
    void Click()
    {
        for (int i = 0; i < Add; i++)
        {
            gameManager.CookieClicked();
        }
    }
    void Update()
    {
        long v = (long)gameManager.Cookies;
        _text.text = $"{v.ToString("#,0")}{_unitName}";
    }
}

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
        FieldInfo info = typeof(GameManager).GetField("_cookieDate", BindingFlags.NonPublic | BindingFlags.Instance);
        var v = (CookieDate)info.GetValue(gameManager);
        _text.text = $"{v.Cookies.ToString("#,0")}{_unitName}";
    }
}

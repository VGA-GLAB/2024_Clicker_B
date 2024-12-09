using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [HideInInspector] 
    public CoinManager Manager;
    
    private NewButton button;
    private Text text;
    
    public string Name;
    public int[] Cost;
    public double[] CoinPerClick;
    public double[] CoinPerSec;

    [SerializeField]
    private GameObject[] _models;

    [Range(0,3)]
    public int level;
    void Awake()
    {
        
        button = GetComponent<NewButton>();
        text = GetComponentInChildren<Text>();
        
        text.raycastTarget = false;
        
    }

    private void Start()
    {
        if (Manager == null)
            Debug.LogError("CoinManager is null");
        button.OnEnter.AddListener(TextUpdate);
        button.OnClick.AddListener(Buy);
        TextUpdate();
    }

    private void Buy()
    {
        if(level >= 3)
            return;
        if (Manager.CanBuy(Cost[level]))
        {
            Manager.BuyBuilding(Cost[level]);
            level++;
            TextUpdate();
        }
    }

    public void TextUpdate()
    {
        if (level >= 3)
            text.text = $"{Name} Max ";
        else
            text.text = $"{Name} Lv{level} {Cost[level]}G";

        _models.ToList().ForEach(x => x?.SetActive(false));
        if(level > 0)
        _models[Mathf.Clamp(level - 1, 0, 2)]?.SetActive(true);
    }
}

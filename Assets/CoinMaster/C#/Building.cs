using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [HideInInspector] 
    public CoinManager Manager;
    
    private NewButton button;
    private Text text;
    
    public string name;
    public int[] cost;
    public double[] CoinPerSec;
    
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
        if (Manager.CanBuy(cost[level]))
        {
            Manager.BuyBuilding(cost[level]);
            level++;
            TextUpdate();
        }
    }

    public void TextUpdate()
    {
        if (level >= 3)
            text.text = $"{name} Max ";
        else
            text.text = $"{name} Lv{level} {cost[level]}G";
    }
}

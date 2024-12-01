using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SaveMachine : MonoBehaviour
{
    public static SaveMachine Instance;
    private readonly string SaveFile = "Save";
    
    [SerializeField] [Tooltip("何秒あたりにオンラインにセーブするか")]
    private readonly float frequencyOfSavesOnline = 30f;

    
    [SerializeField] [Tooltip("何秒あたりにローカルにセーブするか")]
    private readonly float frequencyOfSavesLocal = 10f;
    
    private float savedTimeOnline;
    private float savedTimeLocal;
    
    public SaveData saveData{get; private set;}

    private void Awake()
    {
        Instance = this;
        saveData = LoadLocal();
        Login();
    }

    void Start()
    {
    }

    void Update()
    {
        if (Time.unscaledTime - savedTimeOnline >= frequencyOfSavesOnline)
        {
            savedTimeOnline = Time.unscaledTime;
            SaveOnline();
        }

        if (Time.unscaledTime - savedTimeLocal >= frequencyOfSavesLocal)
        {
            savedTimeLocal = Time.unscaledTime;
            SaveLocal();
        }
    }

    [ContextMenu("LocalLoad")] 
    SaveData LoadLocal()
    {
        return LocalData.Load<SaveData>(SaveFile, null, true) ?? new SaveData();
    }
    
    [ContextMenu("LocalSave")]
    void SaveLocal()
    {
        saveData.Resource = new(CoinManager.Instance.coin);
        Debug.Log(JsonUtility.ToJson(saveData));
        LocalData.Save(SaveFile,saveData,null,true);
    }


    [ContextMenu("OnlineLogin")]
    void Login()
    {
        CoinMasterNetwork.Login();
    }

    [ContextMenu("OnlineSave")]
    void SaveOnline()
    {
        Debug.Log($"before:{CoinManager.Instance.coin}\nafter: {CoinManager.Instance.coin.ToLong()}");
        
        CoinMasterNetwork.UpdateCoin(CoinManager.Instance.coin.ToLong());
        CoinMasterNetwork.UpdateName("0");
        CoinMasterNetwork.UpdateFacility(0);
        CoinMasterNetwork.Save();
    }
}

public class SaveData
{
    public string Name = "User:";
    public Coin Resource = new(0);
    public int Facility = 0;

    [System.Serializable]
    public class Coin
    {
        public string Data;

        public Coin(BigInteger value)
        {
            Data = value.ToString();
        }

        public static implicit operator BigInteger(Coin value)
            => BigInteger.Parse(value.Data);
    }
}

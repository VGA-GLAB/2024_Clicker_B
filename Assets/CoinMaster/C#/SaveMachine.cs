using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using CoinMaster;
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
    public LoginData loginData{get; private set;}

    private void Awake()
    {
        Instance = this;
        saveData = LoadLocal();
        Login();
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
        
        var faci = CoinManager.Instance.buildingsLv();
        saveData.Facility = Encode(faci[0], faci[1], faci[2], faci[3], faci[4]);
        
        Debug.Log(JsonUtility.ToJson(saveData));
        
        LocalData.Save(SaveFile,saveData,null,true);
    }


    [ContextMenu("OnlineLogin")]
    async void Login()
    {
        loginData = await CoinMasterNetwork.Login();
    }

    [ContextMenu("OnlineSave")]
    void SaveOnline()
    {
        //Debug.Log($"before:{CoinManager.Instance.coin}\nafter: {CoinManager.Instance.coin.ToLong()}");

        CoinMasterNetwork.UpdateCoin(CoinManager.Instance.coin.ToLong());
            
        CoinMasterNetwork.UpdateName(loginData.user.name);
        
        var faci = CoinManager.Instance.buildingsLv();
        CoinMasterNetwork.UpdateFacility(Encode(faci[0], faci[1], faci[2], faci[3], faci[4]));
        
        
        CoinMasterNetwork.Save();
        
        /*Debug.Log($"A{faci[0]},{faci[1]},{faci[2]},{faci[3]},{faci[4]}\n" +
                  $"B{Encode(faci[0], faci[1], faci[2], faci[3], faci[4])}\n" +
                  $"C{Decode(Encode(faci[0], faci[1], faci[2], faci[3], faci[4]))}");*/
    }

    public static int Encode(int a, int b, int c, int d, int e)
        => (a << 8) | (b << 6) | (c << 4) | (d << 2) | e;

    public static (int a, int b, int c, int d, int e) Decode(int packed)
        => (
            (packed >> 8) & 0b11,
            (packed >> 6) & 0b11,
            (packed >> 4) & 0b11,
            (packed >> 2) & 0b11,
            packed & 0b11);
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

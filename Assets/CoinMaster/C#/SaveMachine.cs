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
    private bool repair;
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
        saveData.TotalTake = new(CoinManager.Instance.totalTakeCoin);
        
        var faci = CoinManager.Instance.buildingsLv();
        saveData.Facility = Encode(faci,true);
        
        Debug.Log(JsonUtility.ToJson(saveData));
        
        LocalData.Save(SaveFile,saveData,null,true);
    }


    [ContextMenu("OnlineLogin")]
    async void Login()
    {
        repair = true;
        loginData = await CoinMasterNetwork.Login();
        if(loginData.user.name is "noname")
        {
            CoinManager.Instance._panelState = CoinManager.PanelState.NameSetting;
            CoinManager.Instance.PanelActive();
        }
        else
        {
            CoinManager.Instance._panelState = CoinManager.PanelState.InGame;
            CoinManager.Instance.PanelActive();
        }
    }

    [ContextMenu("OnlineSave")]
    void a() => SaveOnline();
    async void SaveOnline(bool? a = false)
    {
        CoinMasterNetwork.UpdateCoin(CoinManager.Instance.coin.ToLong());
        
        var faci = CoinManager.Instance.buildingsLv();
        CoinMasterNetwork.UpdateFacility(Encode(faci, repair));
        
        var result = await CoinMasterNetwork.Save();

        Debug.Log($"{Encode(faci,true)}\n{JsonUtility.ToJson(result)}");
        if (result is null)
            return;
        if ((bool)a)
            return;
        if (!result.isAttackedVillage)
            return;
        CoinManager.Instance.Stolen(result.stolenCoin);
        SaveOnline(true);
    }
    public static int Encode(int[] lv,bool flag)
        => Encode(lv[0], lv[1], lv[2], lv[3], lv[4],flag);

    public static int Encode(int a, int b, int c, int d, int e,bool flag)
        =>(flag ? 1 : 0) << 20 | (a << 16) | (b << 12) | (c << 8) | (d << 4) | e;

    public static (int a, int b, int c, int d, int e,bool flag) Decode(int packed)
        => ((packed >> 16) & 0b11,
            (packed >> 12) & 0b11,
            (packed >> 8) & 0b11,
            (packed >> 4) & 0b11,
            packed & 0b11,
            (packed >> 20) != 0);
    [ContextMenu("AttackMyself")]
    private async void Attack()
    {
        Debug.Log(loginData.user.uuid);
        var result = await CoinMasterNetwork.Attack(loginData.user.uuid, 100);
        Debug.Log(JsonUtility.ToJson(result));
    }
}

public class SaveData
{
    public string Name = "User:";
    public Coin Resource = new(0);
    public Coin TotalTake = new(0);
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

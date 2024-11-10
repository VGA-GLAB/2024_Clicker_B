// Purchase.cs
using UnityEngine;

public class Purchase
{
    private GameManager _gameManager;  // GameManager インスタンス
    private FacilityBase _facility;

    public Purchase(GameManager gameManager, FacilityBase facility)
    {
        _gameManager = gameManager;
        _facility = facility;
    }

    /// <summary>
    /// 施設を購入する
    /// </summary>
    public bool TryPurchaseFacility()
    {
        if (_gameManager.Cookies >= _facility.Cost) 
        {
            _gameManager.Cookies -= _facility.Cost;  // クッキーを減らす
            _facility.Upgrade();  // 施設をアップグレード
            return true;
        }
        return false;
    }

    /// <summary>
    /// アップグレードを購入する
    /// </summary>
    public bool TryPurchaseUpgrade(int cost)
    {
        if (_gameManager.Cookies >= cost) 
        {
            _gameManager.Cookies -= cost;  // クッキーを減らす
            return true;
        }
        return false;
    }
}

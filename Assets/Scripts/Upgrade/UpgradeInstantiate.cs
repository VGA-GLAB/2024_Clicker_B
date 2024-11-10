using UnityEngine;

/// <summary>
/// 現在の施設の数がボーダーラインを超えたら、アイコンを生成する機能
/// </summary>
public class UpgradeInstantiate : MonoBehaviour
{
    [SerializeField] private GameObject _upgradeButton;
    [SerializeField] private Transform _canvas;
    
    //アップグレードのレベル
    private int _cookieLevel, _cursorLevel, _grandmaLevel = 0;

    private void Update()
    {
        int haveCursor = 11; // ToDo:書き換えます。cursorの所持数を取る
        if (haveCursor > UpgradeConditions(_cursorLevel))
        {
            UpgradeUIInst(1, _cursorLevel + 1);
            _cursorLevel++;
        }

        int haveGrandma = 0; // ToDo:書き換えます。グランマの所持数を取る
        if(haveGrandma > UpgradeConditions(_grandmaLevel))
        {
            UpgradeUIInst(2, _grandmaLevel + 1);
            _grandmaLevel++;
        }
    }

    /// <summary>
    /// アップグレードアイコンが生成されるボーダーを返します
    /// </summary>
    private int UpgradeConditions(int currentLevel)
    {
        int border = currentLevel switch
        {
            0 => 10,
            1 => 50,
            2 => 100,
            _ => 0
        };
        return border;
    }

    /// <summary>
    /// アップグレードのアイコンをショップに表示する
    /// </summary>
    private void UpgradeUIInst(int id, int index)
    {
        GameObject upgradeIcon = Instantiate(_upgradeButton, _canvas); //アイコンを生成する
        UpgradeBase upgradeBase = upgradeIcon.GetComponent<UpgradeBase>();
        upgradeBase.IconChange(id, index);
    }
}

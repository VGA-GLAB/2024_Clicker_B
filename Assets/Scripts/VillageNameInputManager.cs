using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 村の名前の入力を受け取るInputFieldを管理するスクリプトです
/// </summary>
public class VillageNameInputManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private InputField _villageNameInputField;

    /// <summary>
    /// 村の名前の更新
    /// </summary>
    public void InputText()
    {
        _gameManager.VillageName = _villageNameInputField.text;
        Debug.Log(_gameManager.VillageName);
    }
    
    //TODO: CSS対策
}

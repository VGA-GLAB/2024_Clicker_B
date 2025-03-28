using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameSettingManager : MonoBehaviour
{
    [SerializeField] private InputField _nameField;
    [SerializeField] private NewButton _applyButton;
    public string VillageName;
    public Action Apply;
    private void Awake()
    {
        _nameField.onEndEdit.AddListener(InputText);
    }
    private void Start()
    {
        _applyButton.OnClick.AddListener(SetName);
    }
    private void InputText(string text)
    {
        Debug.Log(text);
        VillageName = text;
    }
    private void SetName()
    {
        CoinMasterNetwork.UpdateName(VillageName);
        CoinManager.Instance._panelState = CoinManager.PanelState.InGame;
        CoinManager.Instance.PanelActive();
    }
}

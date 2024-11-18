using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class NewButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [field: SerializeField] 
    public bool _isEnter {get; private set; }
    public UnityEvent OnClick {get; private set; }
    public UnityEvent OnEnter {get; private set; }
    public UnityEvent OnExit {get; private set; }
    void Awake()
    {
        _isEnter = false;
        
        if(OnClick == null)
            OnClick = new UnityEvent();
        
        if(OnEnter == null)
            OnEnter = new UnityEvent();
        
        if(OnExit == null)
            OnExit = new UnityEvent();
    }
    void Update()
    {
        if (_isEnter)
            if (Input.GetMouseButtonDown(0))
                OnClick.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isEnter = true;
        OnEnter.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isEnter = false;
        OnExit.Invoke();
    }

    private void OnDisable()
    {
        _isEnter = false;
    }
}

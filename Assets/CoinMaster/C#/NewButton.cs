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

    public UnityEvent OnClicked { get; private set; }
    public UnityEvent OnEnter {get; private set; }
    public UnityEvent OnExit {get; private set; }

    private bool _click;
    void Awake()
    {
        _isEnter = false;
        
        if(OnClick == null)
            OnClick = new UnityEvent();

        if(OnClicked == null) 
            OnClicked = new UnityEvent();
        
        if(OnEnter == null)
            OnEnter = new UnityEvent();
        
        if(OnExit == null)
            OnExit = new UnityEvent();
    }
    void Update()
    {
        if (_isEnter)
            if (Input.GetMouseButtonDown(0))
            {
                OnClick.Invoke();
                _click = true;
            }
        if (_click)
            if (Input.GetMouseButtonUp(0))
            {
                OnClicked.Invoke();
                _click = false;
            }
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

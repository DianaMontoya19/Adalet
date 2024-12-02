using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsUI : BaseUI
{
    [Header("Volume")]
    [SerializeField]
    private List<Slider> _volumeSliders;

    [Header("Back Button")]
    [SerializeField]
    private Button _backButton;

    [SerializeField]
    private UnityEvent _extraBackButtonEvent;

    private void OnEnable()
    {
        _backButton.onClick.AddListener(OnBackButtonClick);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveAllListeners();
    }

    private void OnBackButtonClick()
    {
        _extraBackButtonEvent?.Invoke();
        Deactivate();
    }

    public override void Activate()
    {
        base.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}

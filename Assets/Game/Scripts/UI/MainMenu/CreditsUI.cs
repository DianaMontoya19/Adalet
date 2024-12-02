using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CreditsUI : BaseUI
{
    [Header("Back Button")]
    [SerializeField]
    private Button _backButton;

    [SerializeField]
    private UnityEvent _backButtonEvent;

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
        gameObject.SetActive(false);
        _backButtonEvent?.Invoke();
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

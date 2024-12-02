using UnityEngine;
using UnityEngine.UI;

public abstract class BeginHackingUI : BaseUI
{
    [Header("Buttons")]
    [SerializeField]
    private Button _confirmButton;

    [SerializeField]
    private Button _denyButton;

    private void OnEnable()
    {
        _confirmButton.onClick.AddListener(OnConfirmButtonClick);
        _denyButton.onClick.AddListener(OnDenyButtonClick);
    }

    private void OnDisable()
    {
        _confirmButton.onClick.RemoveAllListeners();
        _denyButton.onClick.RemoveAllListeners();
    }

    protected virtual void OnConfirmButtonClick()
    {
        Deactivate();
    }

    private void OnDenyButtonClick()
    {
        //MLocator.Instance.GameManager.SetGameState(GameState.Exploration);
        Deactivate();
        MLocator.Instance.InputManager.SwitchActionMap(InputStrings.Exploration);
    }

    public override void Activate()
    {
        base.Activate();

        //MLocator.Instance.GameManager.SetGameState(GameState.UI);
        MLocator.Instance.InputManager.SwitchActionMap(InputStrings.UI);
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}

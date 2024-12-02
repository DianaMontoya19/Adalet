using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueUI : BaseUI
{
    [SerializeField]
    private TextMeshProUGUI _dialogueText;

    private InputAction _continueAction;

    private void OnEnable()
    {
        _continueAction.started += OnContinuePressed;
    }

    private void Awake()
    {
        _continueAction = InputSystem.actions.FindAction(InputStrings.Pause);
    }

    private void OnDisable()
    {
        _continueAction.started -= OnContinuePressed;
    }

    private void OnContinuePressed(InputAction.CallbackContext context)
    {
        Deactivate();
    }

    public void SetDialogueText(string text)
    {
        _dialogueText.text = text;
    }

    public override void Activate()
    {
        base.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
        MLocator.Instance.GameManager.SetGameState(GameState.Exploration);
    }
}

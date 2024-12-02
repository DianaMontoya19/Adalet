using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : BaseUI
{
    [Header("Buttons")]
    [SerializeField]
    private Button _continueButton;

    [SerializeField]
    private Button _settingsButton;

    [SerializeField]
    private Button _returnToMenuButton;

    [SerializeField]
    private Button _exitToDesktopButton;

    private Dictionary<string, Button> _pauseButtons;

    [Header("Screens")]
    [SerializeField]
    private Image _settingsScreen;

    [Header("Scene Data")]
    [SerializeField]
    private SceneField _mainMenuScene;

    private void Awake()
    {
        _pauseButtons = new Dictionary<string, Button>()
        {
            { "Continue", _continueButton },
            { "Settings", _settingsButton },
            { "Menu", _returnToMenuButton },
            { "Desktop", _exitToDesktopButton }
        };
    }

    private void OnEnable()
    {
        _pauseButtons["Continue"].onClick.AddListener(OnContinueButtonClick);
        _pauseButtons["Settings"].onClick.AddListener(OnSettingsButtonClick);
        _pauseButtons["Menu"].onClick.AddListener(OnMenuButtonClick);
        _pauseButtons["Desktop"].onClick.AddListener(OnDesktopButtonClick);
    }

    private void OnDisable()
    {
        foreach (Button button in _pauseButtons.Values)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void OnButtonClick()
    {
        ToggleButtonsInteractable(false);
    }

    private void OnContinueButtonClick()
    {
        MLocator.Instance.PauseManager.Unpause();
        Deactivate();
    }

    private void OnSettingsButtonClick()
    {
        Deactivate();
        _settingsScreen.gameObject.SetActive(true);
    }

    private void OnMenuButtonClick()
    {
        MLocator.Instance.PauseManager.Unpause();
        MLocator
            .Instance
            .SceneLoader
            .LoadScene(
                _mainMenuScene,
                onFade: () =>
                {
                    MLocator.Instance.GameManager.SetGameState(GameState.UI);
                    Deactivate();
                },
                onComplete: () =>
                {
                    MLocator.Instance.StartUI.Activate();
                }
            );
    }

    private void OnDesktopButtonClick()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }

    public void ToggleButtonsInteractable(bool isInteractable)
    {
        foreach (Button button in _pauseButtons.Values)
        {
            button.interactable = isInteractable;
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartUI : BaseUI
{
    [Header("Buttons")]
    [SerializeField]
    private Button _startButton;

    [SerializeField]
    private Button _settingsButton;

    [SerializeField]
    private Button _creditsButton;

    [SerializeField]
    private Button _exitButton;
    private Dictionary<string, Button> _menuButtons;

    [Header("Screens")]
    [SerializeField]
    private Image _mainMenuScreen;

    [SerializeField]
    private Image _settingsScreen;

    [SerializeField]
    private Image _creditsScreen;
    private Dictionary<string, Image> _menuScreens;

    [Header("Scene Data")]
    [SerializeField]
    private ExplorationLoadingData _loadingData;

    

    private void Awake()
    {
        _menuButtons = new Dictionary<string, Button>()
        {
            { "Start", _startButton },
            { "Settings", _settingsButton },
            { "Credits", _creditsButton },
            { "Exit", _exitButton }
        };
        _menuScreens = new Dictionary<string, Image>()
        {
            { "MainMenu", _mainMenuScreen },
            { "Settings", _settingsScreen },
            { "Credits", _creditsScreen }
        };
        MLocator.Instance.HackingManager._cinematics.SetActive(false);
    }

    private void OnEnable()
    {
        _menuButtons["Start"].onClick.AddListener(OnStartButtonClick);
        _menuButtons["Settings"].onClick.AddListener(OnSettingsButtonClick);
        _menuButtons["Credits"].onClick.AddListener(OnCreditsButtonClick);
        _menuButtons["Exit"].onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        foreach (Button button in _menuButtons.Values)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void OnButtonClick()
    {
        DeactivateScreens();
        ToggleButtonsInteractable(false);
    }

    private void OnStartButtonClick()
    {
        StartGame();
    }

    private void StartGame()
    {
        
       
        MLocator
            .Instance
            .SceneLoader
            .LoadScene(
                _loadingData.SceneToLoad,
                onFade: () =>
                {
                    MLocator.Instance.PlayerSpawner.SetSpawnID(_loadingData.SpawnID);
                    MLocator.Instance.GameManager.SetGameState(GameState.Exploration);
                   
                    
                    Deactivate();
                },
                onComplete: () =>
                {
                    MLocator.Instance.HackingManager._cinematics.SetActive(true);
                    MLocator.Instance.PlayerSpawner.SetPlayerSpawnPoint();
                    
                }
            );
    }

    private void OnSettingsButtonClick()
    {
        OnButtonClick();

        _menuScreens["Settings"].gameObject.SetActive(true);
    }

    private void OnCreditsButtonClick()
    {
        OnButtonClick();

        _menuScreens["Credits"].gameObject.SetActive(true);
    }

    private void OnExitButtonClick()
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
        foreach (Button button in _menuButtons.Values)
        {
            button.interactable = isInteractable;
        }
    }

    private void DeactivateScreens()
    {
        foreach (Image screen in _menuScreens.Values)
        {
            screen.gameObject.SetActive(false);
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

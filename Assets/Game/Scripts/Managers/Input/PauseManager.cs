using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public bool IsPaused { get; private set; } = false;
    public bool CanPause { get; private set; } = true;

    private InputAction _pauseAction;

    private void OnEnable()
    {
        _pauseAction.started += OnPause;
    }

    private void Awake()
    {
        _pauseAction = InputSystem.actions.FindAction(InputStrings.Pause);
    }

    private void OnDisable()
    {
        _pauseAction.started -= OnPause;
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (CanPause)
        {
            if (!IsPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    private bool ManagePauseState(bool pauseGame)
    {
        IsPaused = pauseGame;
        Time.timeScale = IsPaused ? 0 : 1;
        return IsPaused;
    }

    public void Pause()
    {
        MLocator.Instance.GameManager.SetGameState(GameState.UI);

        ManagePauseState(true);

        MLocator.Instance.PauseUI.Activate();
    }

    public void Unpause()
    {
        MLocator.Instance.GameManager.SetGameState(GameState.Exploration);

        ManagePauseState(false);

        MLocator.Instance.PauseUI.Deactivate();
        MLocator.Instance.SettingsUI.Deactivate();
    }
}
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

public class HackingManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    private PlayerHacking _player;

    [field: Header("Flags")]
    [field: SerializeField]
    public bool IsHacking { get; private set; } = false;

    [field: SerializeField]
    public bool HasCompletedHack { get; private set; } = false;

    [field: SerializeField]
    public bool HasFailedHack { get; private set; } = false;

    [field: Header("Timers")]
    [field: SerializeField, ReadOnly]
    public float RemainingHackTimeSeconds { get; private set; }

    [SerializeField]
    private int _hackDurationSeconds;

    private bool _isTimerRunning;

    [SerializeField]
    private TextMeshProUGUI _hackTimeText;

    [Header("Scenes to Load")]
    [SerializeField, ReadOnly]
    private SceneField _hackingSceneToLoad;

    [SerializeField, ReadOnly]
    private SceneField _explorationSceneToLoad;
    private int _explorationSpawnID;
    HackLevel _currentHack;

    [field: Header("Enemies")]
    [SerializeField, ReadOnly]
    private Pillar[] _pillars;
    private int _pillarAliveCount;

    [SerializeField, ReadOnly]
    private Bouncer[] _bouncers;
    private int _bouncerAliveCount;

    [SerializeField]
    private Animator _transitionAnim;

    [SerializeField] public GameObject _cinematics;

    private void Update()
    {
        if (IsHacking)
        {
            ManageHackTime();

            if (_pillarAliveCount == 0)
            {
                CompleteHacking();
            }
        }
    }

    private void FindEnemies()
    {
        _pillars = FindObjectsByType<Pillar>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        _pillarAliveCount = _pillars.Length;

        _bouncers = FindObjectsByType<Bouncer>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );
        _bouncerAliveCount = _bouncers.Length;
    }
    public void SetUpHacking(ShooterLoadingData hackData)
    {
        _hackingSceneToLoad = hackData.SceneToLoad;
        _explorationSceneToLoad = hackData.SceneToReturn;
        _explorationSpawnID = hackData.SceneToReturnSpawnID;
        _hackDurationSeconds = hackData.HackDurationSeconds;
        _currentHack = hackData.HackLevel;
    }

    public void StartHacking()
    {
        //_hackingSceneToLoad = hackData.SceneToLoad;
        //_explorationSceneToLoad = hackData.SceneToReturn;
        //_explorationSpawnID = hackData.SceneToReturnSpawnID;
        //_hackDurationSeconds = hackData.HackDurationSeconds;
        //_currentHack = hackData.HackLevel;

        MLocator
            .Instance
            .SceneLoader
            .LoadScene(
                _hackingSceneToLoad,
                onFade: () =>
                {
                    MLocator.Instance.GameManager.SetGameState(GameState.Hacking);

                    MLocator.Instance.ShooterHackingUI.Deactivate();

                    MLocator.Instance.PlayerSpawner.SetSpawnID();
                },
                onComplete: () =>
                {
                    MLocator.Instance.PlayerSpawner.SetPlayerSpawnPoint();

                    _player.Health.MaxCurrentHealth();
                    FindEnemies();

                    MLocator.Instance.HackingUI.Activate();
                    RemainingHackTimeSeconds = _hackDurationSeconds;
                    _isTimerRunning = true;
                    IsHacking = true;
                }
            );
    }

    private void EndHacking()
    {
        IsHacking = false;

        MLocator
            .Instance
            .SceneLoader
            .LoadScene(
                _explorationSceneToLoad,
                onFade: () =>
                {
                    DeactivateEnemies();
                    MLocator.Instance.HackingUI.Deactivate();

                    MLocator.Instance.GameManager.SetGameState(GameState.Exploration);

                    MLocator.Instance.PlayerSpawner.SetSpawnID(_explorationSpawnID);

                    _isTimerRunning = false;
                },
                onComplete: () =>
                {
                    MLocator.Instance.PlayerSpawner.SetPlayerSpawnPoint();
                }
            );
    }

    private void CompleteHacking()
    {
        HasCompletedHack = true;
        HasFailedHack = false;

        switch (_currentHack)
        {
            case HackLevel.Entrace:
                MLocator.Instance.GameManager.CompletedEntraceHack = true;
                _cinematics.SetActive(false);
                MLocator
                    .Instance
                    .DialogueUI
                    .SetDialogueText(
                        "Easy peasy, the PAGS security is just as lousy as it is today."
                    );
                MLocator.Instance.DialogueUI.Activate();

                break;
            case HackLevel.Core:
                MLocator.Instance.GameManager.CompletedCoreHack = true;
                MLocator.Instance.DialogueUI.Activate();
                break;
        }

        //* Activate win screen transition

        EndHacking();
    }

    public void FailHacking()
    {
        HasFailedHack = true;
        HasCompletedHack = false;

        //* Activate fail screen transition

        EndHacking();
    }

    public void DeactivateEnemies()
    {
        foreach (Pillar pillar in _pillars)
        {
            pillar.Deactivate();
        }
        foreach (Bouncer bouncer in _bouncers)
        {
            bouncer.Deactivate();
        }
    }

    public void LowerAliveTowerCount()
    {
        --_pillarAliveCount;
    }

    private void ManageHackTime()
    {
        if (_isTimerRunning)
        {
            if (RemainingHackTimeSeconds > 0)
            {
                RemainingHackTimeSeconds -= Time.deltaTime;

                DisplayTime(RemainingHackTimeSeconds);
            }
            else
            {
                FailHacking();
                RemainingHackTimeSeconds = 0;
                _isTimerRunning = false;
            }
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = timeToDisplay % 1 * 1000;

        _hackTimeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
    }

    internal void SetUpHacking(
        SceneField sceneToLoad,
        SceneField sceneToReturn,
        object sceneToReturnSpawnId,
        int hackDurationSeconds,
        HackLevel hackLevel
    )
    {
        throw new NotImplementedException();
    }
}

public enum HackLevel
{
    Entrace,
    Core
}

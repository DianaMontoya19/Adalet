using UnityEngine;

public class GameManager : MonoBehaviour
{
    [field: Header("Game State")]
    [field: SerializeField]
    public GameState GameState { get; private set; } = GameState.Exploration;

    [Space(5)]
    [Header("Player Data")]
    [SerializeField]
    private PlayerExploration _explorationPlayer;

    [SerializeField]
    private PlayerHacking _hackingPlayer;

    [field: SerializeField, ReadOnly]
    public Player ActivePlayer { get; private set; }

    [field: Space(5)]
    [field: Header("General Flags")]
    [field: SerializeField]
    public bool HasEnteredPassword = false;

    [field: SerializeField]
    public bool HasSolvedSinging = false;

    [field: SerializeField]
    public bool HasSolvedDancing = false;

    [field: Space(5)]
    [field: Header("Hacking Flags")]
    [field: SerializeField]
    public bool CompletedEntraceHack = false;

    [field: SerializeField]
    public bool CompletedCoreHack = false;

    [field: Space(5)]
    [field: Header("Key Items Flags")]
    [field: SerializeField]
    public bool HasRepairKit = false;

    [field: SerializeField]
    public bool HasFacialID = false;

    [field: SerializeField]
    public bool HasCoreCode = false;

    private void OnEnable()
    {
        SetGameState(GameState);
    }

    public void SetGameState(GameState newGameState)
    {
        GameState = newGameState;
        SetActivePlayer();
    }

    private void SetActivePlayer()
    {
        DisableAllPlayers();

        switch (GameState)
        {
            case GameState.Exploration:
            case GameState.Dancing:
                EnablePlayerExploration();
                MLocator.Instance.MouseVisibilityManager.HideMouse();
                break;
            case GameState.UI:
                MLocator.Instance.MouseVisibilityManager.ShowMouse();
                break;
            case GameState.Hacking:
                EnablePlayerHacking();
                MLocator.Instance.MouseVisibilityManager.ShowMouse();
                break;
        }
    }

    private void EnablePlayerExploration()
    {
        _explorationPlayer.gameObject.SetActive(true);
        ActivePlayer = _explorationPlayer;
        MLocator.Instance.InputManager.SwitchActionMap(InputStrings.Exploration);
    }

    private void EnablePlayerHacking()
    {
        _hackingPlayer.gameObject.SetActive(true);
        ActivePlayer = _hackingPlayer;
        MLocator.Instance.InputManager.SwitchActionMap(InputStrings.Hacking);
    }

    public void DisableAllPlayers()
    {
        _explorationPlayer.gameObject.SetActive(false);
        _hackingPlayer.gameObject.SetActive(false);
    }
}

public enum GameState
{
    Exploration,
    Hacking,
    Dancing,
    UI,
}

using UnityEngine;

public class MLocator : Singleton<MLocator>
{
    [field: Header("Managers"), Space(5)]
    [field: SerializeField]
    public GameManager GameManager { get; private set; }

    [field: SerializeField]
    public InputManager InputManager { get; private set; }

    [field: SerializeField]
    public PauseManager PauseManager { get; private set; }

    [field: SerializeField]
    public MouseVisibilityManager MouseVisibilityManager { get; private set; }

    [field: SerializeField]
    public HackingManager HackingManager { get; private set; }

    [field: Header("Loading"), Space(5)]
    [field: SerializeField]
    public SceneLoader SceneLoader { get; private set; }

    [field: SerializeField]
    public PlayerSpawner PlayerSpawner { get; private set; }

    [field: Header("Sound"), Space(5)]
    [field: SerializeField]
    public AudioManager AudioManager { get; private set; }

    [field: SerializeField]
    public FMODEvents FMODEvents { get; private set; }

    [field: Header("UI"), Space(5)]
    [field: SerializeField]
    public StartUI StartUI { get; private set; }

    [field: SerializeField]
    public InteractionUI InteractionUI { get; private set; }

    [field: SerializeField]
    public SettingsUI SettingsUI { get; private set; }

    [field: SerializeField]
    public PauseUI PauseUI { get; private set; }

    [field: SerializeField]
    public ClueUI ClueUI { get; private set; }

    [field: SerializeField]
    public DialogueUI DialogueUI { get; private set; }

    [field: Header("Puzzles"), Space(5)]
    [field: SerializeField]
    public ShooterHackingUI ShooterHackingUI { get; private set; }

    [field: SerializeField]
    public HackingUI HackingUI { get; private set; }

    [field: SerializeField]
    public PuzzleHackingUI PuzzleHackingUI { get; private set; }

    [field: SerializeField]
    public MusicPuzzleManager MusicPuzzleManager { get; private set; }

    [field: SerializeField]
    public RhythmPuzzleManager RhythmPuzzleManager { get; private set; }
}

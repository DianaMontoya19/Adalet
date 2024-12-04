using System.Collections;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class RhythmPuzzleManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private InteractionTrigger _trigger;

    [SerializeField]
    private GameObject _container;

    [SerializeField]
    private GameObject _closedDoor;
    private DanceArrowSpawner _danceArrowSpawner;

    [Header("FMOD Events")]
    private EventInstance _danceMusic;
    private EventInstance _alarm;

    [SerializeField]
    private Animator _danceAnimator;

    [Header("Actions")]
    private InputAction _upAction;
    private InputAction _downAction;
    private InputAction _leftAction;
    private InputAction _rightAction;

    [Header("Buttons")]
    [SerializeField]
    private DanceButton _upButton;

    [SerializeField]
    private DanceButton _downButton;

    [SerializeField]
    private DanceButton _leftButton;

    [SerializeField]
    private DanceButton _rightButton;

    [Header("Button Sprites")]
    [SerializeField]
    private Sprite _defaultSprite;

    [SerializeField]
    private Sprite _pressedSprite;

    [Header("Timelines")]
    [SerializeField]
    private PlayableDirector _winTimeline;

    [SerializeField]
    private PlayableDirector _loseTimeline;

    [SerializeField, ReadOnly]
    private float _correctNoteCount;

    public bool IsDancing { get; private set; }
    public bool CanSpawn { get; private set; }

    private void Awake()
    {
        _danceArrowSpawner = GetComponent<DanceArrowSpawner>();

        _leftAction = InputSystem.actions.FindAction(InputStrings.Left);
        _upAction = InputSystem.actions.FindAction(InputStrings.Up);
        _downAction = InputSystem.actions.FindAction(InputStrings.Down);
        _rightAction = InputSystem.actions.FindAction(InputStrings.Right);
    }

    private void OnEnable()
    {
        _upAction.started += SetUpPressedButton;
        _upAction.canceled += SetUpPressedButton;

        _downAction.started += SetDownPressedButton;
        _downAction.canceled += SetDownPressedButton;

        _leftAction.started += SetLeftPressedButton;
        _leftAction.canceled += SetLeftPressedButton;

        _rightAction.started += SetRightPressedButton;
        _rightAction.canceled += SetRightPressedButton;
    }

    private void Start()
    {
        _danceMusic = MLocator
            .Instance
            .AudioManager
            .CreateInstance(MLocator.Instance.FMODEvents.DanceMusic);

        _alarm = MLocator.Instance.AudioManager.CreateInstance(MLocator.Instance.FMODEvents.Alarm);

        StartDancePuzzle();
    }

    private void OnDisable()
    {
        _upAction.started -= SetUpPressedButton;
        _upAction.canceled -= SetUpPressedButton;

        _downAction.started -= SetDownPressedButton;
        _downAction.canceled -= SetDownPressedButton;

        _leftAction.started -= SetLeftPressedButton;
        _leftAction.canceled -= SetLeftPressedButton;

        _rightAction.started -= SetRightPressedButton;
        _rightAction.canceled -= SetRightPressedButton;
    }

    public void StartDancePuzzle()
    {
        IsDancing = true;

        _danceMusic.start();

        _correctNoteCount = 0;

        StartCoroutine(SpawnArrows());
    }

    private IEnumerator SpawnArrows()
    {
        while (IsDancing)
        {
            _danceMusic.getPlaybackState(out PLAYBACK_STATE state);
            if (state.Equals(PLAYBACK_STATE.STOPPED))
            {
                EndDancePuzzle();
            }

            _danceArrowSpawner.DanceArrowPool.Get();

            CanSpawn = false;
            yield return Yielders.WaitForSeconds(2.0f);
            CanSpawn = true;
        }
    }

    public void EndDancePuzzle()
    {
        IsDancing = false;
        if (_correctNoteCount >= 90)
        {
            StartCoroutine(WinPuzzle());
        }
        else
        {
            StartCoroutine(FailPuzzle());
        }
    }

    private IEnumerator WinPuzzle()
    {
        _winTimeline.gameObject.SetActive(true);
        _alarm.setParameterByName("WinOrLose", 0);
        _alarm.start();

        yield return Yielders.WaitForSeconds(3.5f);

        MLocator.Instance.GameManager.HasFacialID = true;

        _container.SetActive(false);

        _closedDoor.SetActive(false);
        _trigger.Deactivate();

        MLocator
            .Instance
            .DialogueUI
            .SetDialogueText(
                "Did you like my dance little old camera? Because I sure did, almost as much as finding your secrets."
            );
        MLocator.Instance.DialogueUI.Activate();

        MLocator.Instance.GameManager.SetGameState(GameState.Exploration);
    }

    private IEnumerator FailPuzzle()
    {
        _loseTimeline.gameObject.SetActive(true);

        yield return Yielders.WaitForSeconds(3.5f);

        _container.SetActive(false);

        MLocator.Instance.GameManager.SetGameState(GameState.Exploration);
        MLocator.Instance.InteractionUI.Activate();
    }

    private void SetUpPressedButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PressButton(_upButton);
            
        }
        else
        {
            ReleaseButton(_upButton);
        }
    }

    private void SetDownPressedButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PressButton(_downButton);
        }
        else
        {
            ReleaseButton(_downButton);
        }
    }

    private void SetLeftPressedButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PressButton(_leftButton);
        }
        else
        {
            ReleaseButton(_leftButton);
        }
    }

    private void SetRightPressedButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PressButton(_rightButton);
        }
        else
        {
            ReleaseButton(_rightButton);
        }
    }

    private void PressButton(DanceButton button)
    {
        button.Renderer.sprite = _pressedSprite;
        if (button.IsArrowInside)
        {
            NoteHit();
        }
        else
        {
            NoteMissed();
        }
    }

    private void ReleaseButton(DanceButton button)
    {
        button.Renderer.sprite = _defaultSprite;
    }

    public void NoteHit()
    {
        _alarm.setParameterByName("WinOrLose", 0);
        _alarm.start();

        _danceAnimator.enabled = true;
        _correctNoteCount++;
    }

    public void NoteMissed()
    {
        _alarm.setParameterByName("WinOrLose", 1);
        _alarm.start();

        _danceAnimator.enabled = false;
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

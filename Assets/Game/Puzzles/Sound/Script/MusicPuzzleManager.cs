using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class MusicPuzzleManager : BaseUI
{
    [Header("Objects")]
    [SerializeField]
    private InteractionTrigger _trigger;

    [Header("Buttons")]
    [SerializeField]
    private Button _doButton;

    [SerializeField]
    private Button _reButton;

    [SerializeField]
    private Button _miButton;

    [SerializeField]
    private Button _faButton;

    [SerializeField]
    private Button _solButton;

    [SerializeField]
    private Button _laButton;

    [SerializeField]
    private Button _siButton;
    private List<Button> _buttons;

    [Header("FMOD Events")]
    private EventInstance _musicNote;
    private EventInstance _alarm;

    [Header("Note Order")]
    [SerializeField]
    private List<Note> _targetOrder;

    private List<Note> _currentOrder = new();

    [Header("Animation")]
    [SerializeField]
    private List<Animator> _barAnimators;

    [Header("Life Images")]
    [SerializeField]
    private GameObject[] _life;

    [Header("Timelines")]
    [SerializeField]
    private PlayableDirector _winTimeline;

    [SerializeField]
    private PlayableDirector _loseTimeline;

    private int _currentIndex = 0;
    private int _failCount = 0;

    public bool IsSinging { get; private set; }

    private void Start()
    {
        _musicNote = MLocator
            .Instance
            .AudioManager
            .CreateInstance(MLocator.Instance.FMODEvents.MusicNotes);

        _alarm = MLocator.Instance.AudioManager.CreateInstance(MLocator.Instance.FMODEvents.Alarm);
    }

    private void OnEnable()
    {
        _buttons = new()
        {
            _doButton,
            _reButton,
            _miButton,
            _faButton,
            _solButton,
            _laButton,
            _siButton,
        };

        _doButton.onClick.AddListener(() => PlaySound(Note.DO));
        _reButton.onClick.AddListener(() => PlaySound(Note.RE));
        _miButton.onClick.AddListener(() => PlaySound(Note.MI));
        _faButton.onClick.AddListener(() => PlaySound(Note.FA));
        _solButton.onClick.AddListener(() => PlaySound(Note.SOL));
        _laButton.onClick.AddListener(() => PlaySound(Note.LA));
        _siButton.onClick.AddListener(() => PlaySound(Note.SI));
    }

    private void OnDisable()
    {
        foreach (Button button in _buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    public void StartMusicPuzzle()
    {
        IsSinging = true;

        _winTimeline.gameObject.SetActive(false);
        _loseTimeline.gameObject.SetActive(false);

        _currentOrder.Clear();
        _currentIndex = 0;

        /* _failCount = 0; */
    }

    private void PlaySound(Note note)
    {
        _currentOrder.Add(note);

        if (note == _targetOrder[_currentIndex])
        {
            _musicNote.setParameterByName("Note", (int)note);
            _musicNote.start();

            _barAnimators[_currentIndex].enabled = true;
            _currentIndex++;
        }
        else
        {
            if (_failCount >= 3)
            {
                StartCoroutine(FailPuzzle());
            }
            else
            {
                _currentOrder.RemoveAt(_currentOrder.Count - 1);

                _alarm.setParameterByName("WinOrLose", 1);
                _alarm.start();

                _musicNote.stop(STOP_MODE.ALLOWFADEOUT);
                /* _life[_failCount].SetActive(true);
                _failCount++; */
            }
        }

        if (_currentIndex >= _targetOrder.Count)
        {
            StartCoroutine(WinPuzzle());
        }
    }

    private IEnumerator WinPuzzle()
    {
        _winTimeline.gameObject.SetActive(true);
        _alarm.setParameterByName("WinOrLose", 0);
        _alarm.start();

        yield return Yielders.WaitForSeconds(3.5f);

        MLocator.Instance.GameManager.HasFacialID = true;

        Deactivate();
        _trigger.Deactivate();

        MLocator
            .Instance
            .DialogueUI
            .SetDialogueText(
                "Thanks for the birthday gift doctor, your face data shall be useful."
            );
        MLocator.Instance.DialogueUI.Activate();
        MLocator.Instance.GameManager.HasSolvedSinging = true;
        MLocator.Instance.GameManager.SetGameState(GameState.Exploration);
       
    }

    private IEnumerator FailPuzzle()
    {
        _loseTimeline.gameObject.SetActive(true);

        yield return Yielders.WaitForSeconds(3.5f);

        Deactivate();
        MLocator.Instance.GameManager.SetGameState(GameState.Exploration);
        MLocator.Instance.InteractionUI.Activate();
    }
}

public enum Note
{
    DO,
    RE,
    MI,
    FA,
    SOL,
    LA,
    SI,
}

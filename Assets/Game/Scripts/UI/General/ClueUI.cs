using UnityEngine;
using UnityEngine.UI;

public class ClueUI : BaseUI
{
    [Header("Clues")]
    [SerializeField]
    private RenderTexture _imgBirthday;

    [SerializeField]
    private RenderTexture _imgLocker;

    [SerializeField]
    private RenderTexture _imgPassword;

    [SerializeField]
    private RawImage _clueImage;

    [Header("Button")]
    [SerializeField]
    private Button _backButton;

    private void OnEnable()
    {
        _backButton.onClick.AddListener(Deactivate);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveAllListeners();
    }

    public void SelectClue(Clue clue)
    {
        switch (clue)
        {
            case Clue.Birthday:
                _clueImage.texture = _imgBirthday;
                break;
            case Clue.Locker:
                _clueImage.texture = _imgLocker;
                break;
            case Clue.Password:
                _clueImage.texture = _imgPassword;
                break;
        }
    }

    public override void Activate()
    {
        base.Activate();
        MLocator.Instance.InteractionUI.Deactivate();
        MLocator.Instance.GameManager.SetGameState(GameState.UI);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        MLocator.Instance.GameManager.SetGameState(GameState.Exploration);
    }
}

public enum Clue
{
    Birthday,
    Locker,
    Password
}

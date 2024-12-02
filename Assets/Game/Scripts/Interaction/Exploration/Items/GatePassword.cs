using UnityEngine;

public class GatePassword : InteractableObject
{
    [SerializeField]
    private InteractionTrigger _interactionTrigger;
    [SerializeField]
    private Clue _clue;

    public override void Interact()
    {
        base.Interact();
        MLocator.Instance.GameManager.HasEnteredPassword = true;
        MLocator.Instance.GameManager.SetGameState(GameState.UI);
        MLocator.Instance.ClueUI.SelectClue(_clue);
        MLocator.Instance.ClueUI.Activate();
        _interactionTrigger.Deactivate();
    }
}

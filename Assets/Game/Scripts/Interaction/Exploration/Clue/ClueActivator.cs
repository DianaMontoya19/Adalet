using UnityEngine;

public class ClueActivator : InteractableObject
{
    [SerializeField]
    private Clue _clue;

    public override void Interact()
    {
        base.Interact();
        MLocator.Instance.GameManager.SetGameState(GameState.UI);
        MLocator.Instance.ClueUI.SelectClue(_clue);
        MLocator.Instance.ClueUI.Activate();
    }
}

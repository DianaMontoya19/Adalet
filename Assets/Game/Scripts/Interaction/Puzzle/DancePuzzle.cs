using UnityEngine;

public class DancePuzzle : PuzzleInteract
{
    [SerializeField]
    private GameObject _puzzleContainer;

    public override void Interact()
    {
        if (MLocator.Instance.GameManager.HasFacialID)
        {
            base.Interact();
            MLocator.Instance.InteractionUI.Deactivate();
            MLocator.Instance.GameManager.SetGameState(GameState.Dancing);
        }
        else
        {
            MLocator
                .Instance
                .DialogueUI
                .SetDialogueText(
                    "Locked again, I'm going to need some sort of facial ID information for this one."
                );
            MLocator.Instance.DialogueUI.Activate();
        }
    }
}

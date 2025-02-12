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
            _puzzleUI.Activate();
            MLocator.Instance.InteractionUI.Deactivate();
            MLocator.Instance.GameManager.SetGameState(GameState.UI);
            
        }
        else
        {
            MLocator
                .Instance
                .DialogueUI
                .SetDialogueText(
                    "Locked again, I'm going to need some sort of facial ID information for this one.  Press ESC"
                );
            MLocator.Instance.DialogueUI.Activate();
        }
    }
}

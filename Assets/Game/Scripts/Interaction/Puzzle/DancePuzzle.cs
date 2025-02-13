using UnityEngine;

public class DancePuzzle : PuzzleInteract
{
    [SerializeField]
    private GameObject _puzzleContainer;

    public override void Interact()
    {
        if (MLocator.Instance.GameManager.HasFacialID)
        {
            MLocator.Instance.InteractionUI.Deactivate();
            MLocator.Instance.GameManager.SetGameState(GameState.Dancing);
            _puzzleContainer.SetActive(true);
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

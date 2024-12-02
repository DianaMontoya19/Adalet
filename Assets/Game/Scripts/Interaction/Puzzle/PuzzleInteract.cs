using UnityEngine;

public abstract class PuzzleInteract : InteractableObject
{
    [SerializeField]
    protected BaseUI _puzzleUI;

    public override void Interact()
    {
        base.Interact();
        _puzzleUI.Activate();
    }
}

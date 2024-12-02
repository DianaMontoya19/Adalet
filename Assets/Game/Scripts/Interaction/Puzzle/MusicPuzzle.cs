using UnityEngine;

public class MusicPuzzle : PuzzleInteract
{
    public override void Interact()
    {
        base.Interact();
        _puzzleUI.Activate();
        MLocator.Instance.InteractionUI.Deactivate();
        MLocator.Instance.GameManager.SetGameState(GameState.UI);
        MLocator.Instance.MusicPuzzleManager.StartMusicPuzzle();
    }
}

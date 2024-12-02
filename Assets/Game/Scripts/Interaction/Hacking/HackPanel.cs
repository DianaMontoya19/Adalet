using UnityEngine;

public class HackPanel : InteractableObject
{
    [SerializeField]
    private ShooterLoadingData _loadData;

    public override void Interact()
    {
        base.Interact();

        MLocator.Instance.GameManager.SetGameState(GameState.UI);
        MLocator.Instance.InteractionUI.Deactivate();

        MLocator.Instance.ShooterHackingUI.Activate();
        MLocator.Instance.ShooterHackingUI.SetHackData(_loadData);
    }
}

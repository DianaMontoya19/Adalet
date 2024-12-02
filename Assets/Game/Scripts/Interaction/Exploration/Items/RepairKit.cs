using UnityEngine;

public class RepairKit : InteractableObject
{
    [SerializeField]
    private InteractionTrigger _interactionTrigger;

    public override void Interact()
    {
        base.Interact();
        MLocator.Instance.GameManager.HasRepairKit = true;
        _interactionTrigger.Deactivate();
    }
}

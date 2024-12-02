using UnityEngine;

public class CoreCorridorDoor : Door
{
    public override void Interact()
    {
        if (MLocator.Instance.GameManager.HasRepairKit)
        {
            base.Interact();
        }
        else
        {
            MLocator
                .Instance
                .DialogueUI
                .SetDialogueText("It's completly stuck, seems like age got to this one.");
            MLocator.Instance.DialogueUI.Activate();
        }
    }
}

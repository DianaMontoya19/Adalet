using UnityEngine;

public class FloatingRoomsDoor : Door
{
    public override void Interact()
    {
        if (MLocator.Instance.GameManager.HasEnteredPassword)
        {
            base.Interact();
        }
        else
        {
            MLocator
                .Instance
                .DialogueUI
                .SetDialogueText("No luck with this door either, I'll need some sort of password.");
            MLocator.Instance.DialogueUI.Activate();
        }
    }
}

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
                .SetDialogueText(
                    "The door won't budge, seems like the opening mechanisim broke at some point. Maybe I can repair it."
                );
            MLocator.Instance.DialogueUI.Activate();
        }
    }
}

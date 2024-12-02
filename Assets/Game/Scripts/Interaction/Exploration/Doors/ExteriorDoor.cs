using UnityEngine;

public class ExteriorDoor : Door
{
    public override void Interact()
    {
        if (MLocator.Instance.GameManager.CompletedEntraceHack)
        {
            base.Interact();
        }
        else
        {
            MLocator
                .Instance
                .DialogueUI
                .SetDialogueText(
                    "Damn, apparently some of the security is still up. Some quick graffitiing should do the trick."
                );
            MLocator.Instance.DialogueUI.Activate();
        }
    }
}

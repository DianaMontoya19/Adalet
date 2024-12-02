using UnityEngine;

public class Door : InteractableObject
{
    [field: SerializeField]
    public bool IsLocked { get; private set; } = false;

    [SerializeField]
    private ExplorationLoadingData _loadData;

    public override void Interact()
    {
        if (!IsLocked)
        {
            base.Interact();
            MLocator.Instance.GameManager.SetGameState(GameState.UI);
            MLocator.Instance.InteractionUI.Deactivate();

            MLocator
                .Instance
                .SceneLoader
                .LoadScene(
                    _loadData.SceneToLoad,
                    onFade: () =>
                    {
                        MLocator.Instance.PlayerSpawner.SetSpawnID(_loadData.SpawnID);
                    },
                    onComplete: () =>
                    {
                        MLocator.Instance.PlayerSpawner.SetPlayerSpawnPoint();
                    }
                );
        }
        else
        {
            MLocator
                .Instance
                .DialogueUI
                .SetDialogueText("The door won't budge, seems to be locked from the inside.");
            MLocator.Instance.DialogueUI.Activate();
        }
    }
}

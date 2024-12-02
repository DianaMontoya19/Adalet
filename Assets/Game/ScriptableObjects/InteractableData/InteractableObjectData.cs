using UnityEngine;

[CreateAssetMenu(
    fileName = "InteractableObject",
    menuName = "Adalet/Interaction/Interactable Object"
)]
public class InteractableObjectData : ScriptableObject
{
    public string Name;
    public string Message;
}

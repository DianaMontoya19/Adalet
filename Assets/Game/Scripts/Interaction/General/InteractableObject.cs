using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteraction
{
    public virtual void Interact() { }
}

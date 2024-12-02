using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [field: SerializeField]
    public Rigidbody RB { get; private set; }

    [field: SerializeField]
    public Collider Collider { get; private set; }

    private void Awake()
    {
        gameObject.tag = TagStrings.Player;
        gameObject.layer = LayerMask.NameToLayer(LayerStrings.Player);
    }
}

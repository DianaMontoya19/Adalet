using UnityEngine;

[RequireComponent(typeof(Health))]
public class BreakableWall : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        gameObject.layer = LayerMask.NameToLayer(LayerStrings.BreakableWall);
    }
}

using UnityEngine;

public class Pillar : Enemy
{
    public override void Deactivate()
    {
        base.Deactivate();
        MLocator.Instance.HackingManager.LowerAliveTowerCount();
    }
}

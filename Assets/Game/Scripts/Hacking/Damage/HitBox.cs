using UnityEngine;

public abstract class HitBox : MonoBehaviour
{
    protected Collider _hitBoxCollider;

    private void Awake()
    {
        _hitBoxCollider = GetComponent<Collider>();
        _hitBoxCollider.isTrigger = true;
    }

    protected virtual void OnTriggerEnter(Collider collider) { }
}

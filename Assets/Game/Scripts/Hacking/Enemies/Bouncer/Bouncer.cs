using PrimeTween;
using UnityEngine;

public class Bouncer : Enemy
{
    [Header("Bounce")]
    [SerializeField, ReadOnly]
    private Vector3 _lastDirection;

    private void Start()
    {
        ApplyInitialVelocity();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint collisionContact = collision.GetContact(0);
        LayerMask layer = collisionContact.otherCollider.gameObject.layer;

        if (
            layer == LayerMask.NameToLayer(LayerStrings.HackWall)
            || layer == LayerMask.NameToLayer(LayerStrings.BreakableWall)
            || layer == LayerMask.NameToLayer(LayerStrings.Enemy)
        )
        {
            Bounce(collisionContact);
        }
    }

    private void ApplyInitialVelocity()
    {
        _lastDirection = new(Random.Range(-10.0f, 10.0f), 0.0f, Random.Range(-10.0f, 10.0f));

        _rb.linearVelocity = _lastDirection.normalized * Data.MoveSpeed;
        _lastDirection = _rb.linearVelocity.normalized;
    }

    private void Bounce(ContactPoint collisionContact)
    {
        Vector3 direction = Vector3.Reflect(_lastDirection.normalized, collisionContact.normal);

        _rb.linearVelocity = Data.MoveSpeed * direction;

        _lastDirection = _rb.linearVelocity;
    }
}

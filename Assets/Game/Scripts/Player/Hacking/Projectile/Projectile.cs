using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    public Rigidbody RB { get; private set; }
    private ObjectPool<Projectile> _projectilePool;

    public ProjectileData Data;

    private void Awake()
    {
        gameObject.tag = TagStrings.Projectile;

        RB = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(DeactivateProjectileAfterTimeCoorutine());
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer(LayerStrings.HackWall))
        {
            _projectilePool.Release(this);
        }
        else if (collider.TryGetComponent<IDamageable>(out IDamageable iDamageable))
        {
            iDamageable.Damage(Data.Damage);
            _projectilePool.Release(this);
        }
    }

    public void AddForceToProjectile(Vector3 forward)
    {
        Vector3 force = forward * Data.Speed;
        RB.AddForce(force, ForceMode.Impulse);
    }

    public void SetPool(ObjectPool<Projectile> pool)
    {
        _projectilePool = pool;
    }

    private IEnumerator DeactivateProjectileAfterTimeCoorutine()
    {
        if (this.enabled)
        {
            float elapsedTime = 0.0f;
            while (elapsedTime < Data.DestroyTime)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            _projectilePool.Release(this);
        }
    }
}

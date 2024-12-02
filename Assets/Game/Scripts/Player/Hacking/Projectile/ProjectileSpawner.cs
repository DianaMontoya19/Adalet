using UnityEngine;
using UnityEngine.Pool;

public class ProjectileSpawner : MonoBehaviour
{
    public ObjectPool<Projectile> ProjectilePool;

    private HackingShoot _hackingShoot;

    [SerializeField]
    private Transform _projectileHolder;

    private void Start()
    {
        _hackingShoot = GetComponent<HackingShoot>();
        ProjectilePool = new ObjectPool<Projectile>(
            CreateProjectile,
            OnTakeProjectileFromPool,
            OnReturnProjectileFromPool,
            OnDestroyProjectile,
            true,
            50,
            1000
        );
    }

    private Projectile CreateProjectile()
    {
        // Create projectile
        Projectile projectile = Instantiate(
            _hackingShoot.Projectile,
            _hackingShoot.ProjectileSpawn.position,
            Quaternion.identity,
            _projectileHolder
        );

        // Assign projectile pool
        projectile.SetPool(ProjectilePool);

        return projectile;
    }

    private void OnTakeProjectileFromPool(Projectile projectile)
    {
        projectile.transform.position = _hackingShoot.ProjectileSpawn.position;
        projectile.transform.forward = _hackingShoot.ProjectileSpawn.forward;

        projectile.gameObject.SetActive(true);

        projectile.AddForceToProjectile(transform.forward);
    }

    private void OnReturnProjectileFromPool(Projectile projectile)
    {
        projectile.RB.linearVelocity = Vector3.zero;

        projectile.gameObject.SetActive(false);
    }

    private void OnDestroyProjectile(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }
}

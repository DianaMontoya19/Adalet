using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class HackingShoot : MonoBehaviour
{
    [field: SerializeField]
    public Projectile Projectile { get; private set; }

    [field: SerializeField]
    public Transform ProjectileSpawn { get; private set; }

    private ProjectileSpawner _projectileSpawner;

    private bool _canShoot = true;

    private InputAction _shootAction;

    private void Awake()
    {
        _projectileSpawner = GetComponent<ProjectileSpawner>();

        _shootAction = InputSystem.actions.FindAction(InputStrings.Shoot);
    }

    private void Update()
    {
        if (_canShoot && _shootAction.IsPressed())
        {
            StartCoroutine(ShootProjectile());
        }
    }

    private IEnumerator ShootProjectile()
    {
        _canShoot = false;

        _projectileSpawner.ProjectilePool.Get();

        yield return Yielders.WaitForSeconds(Projectile.Data.ShotDelay);

        _canShoot = true;
    }
}

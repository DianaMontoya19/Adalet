using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Health))]
public abstract class Enemy : MonoBehaviour, IDamageable
{
    public EnemyData Data;

    protected Rigidbody _rb;
    protected Health _health;
    protected EnemyHitBox _hitBox;
    [SerializeField] private GameObject _explotion;

    protected virtual void Awake()
    {
        gameObject.tag = TagStrings.Enemy;
        gameObject.layer = LayerMask.NameToLayer(LayerStrings.Enemy);

        _rb = GetComponent<Rigidbody>();
        _health = GetComponent<Health>();
        _hitBox = GetComponentInChildren<EnemyHitBox>();

        _health.SetMaxHealth(Data.MaxHealth);
        _health.ChangeCurrentHealth(Data.MaxHealth);
        _health.SetInvicibilityDuration(Data.InvicibilityDurationSeconds);
    }

    public virtual void Damage(int damageValue)
    {
        if (!_health.IsInvicible)
        {
            _health.ChangeCurrentHealth(-damageValue);
            _explotion.SetActive(false);
            StartCoroutine(_health.StartInvincibilty());
            _explotion.SetActive(true);

            if (_health.CurrentHealth <= 0)
            {
                _health.SetIsAlive(false);
                Deactivate();
            }
        }
    }

    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

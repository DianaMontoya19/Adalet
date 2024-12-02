using UnityEngine;

public class PlayerHacking : Player, IDamageable
{
    public HackingMovement Movement { get; private set; }
    public PlayerHitBox HitBox { get; private set; }

    public Health Health { get; private set; }

    private void Awake()
    {
        Movement = GetComponent<HackingMovement>();
        HitBox = GetComponent<PlayerHitBox>();
        Health = GetComponent<Health>();
    }

    public void Damage(int damageValue)
    {
        if (!Health.IsInvicible)
        {
            Health.ChangeCurrentHealth(-damageValue);
            StartCoroutine(Health.StartInvincibilty());
            if (Health.CurrentHealth <= 0)
            {
                Health.SetIsAlive(false);
                Deactivate();
            }
        }
    }

    public void Deactivate()
    {
        MLocator.Instance.HackingManager.FailHacking();
    }
}

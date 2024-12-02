using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private int _maxHealth;

    [field: SerializeField]
    public int CurrentHealth { get; private set; }

    [field: SerializeField]
    public bool IsAlive { get; private set; } = true;

    [field: Header("Invicibility")]
    [field: SerializeField]
    public bool IsInvicible { get; private set; }

    [SerializeField]
    private float _invicibilityDurationSeconds;

    private void OnEnable()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        MaxCurrentHealth();
        IsAlive = true;
        IsInvicible = false;
    }

    public void SetMaxHealth(int healthValue)
    {
        _maxHealth = healthValue;
    }

    public void ChangeCurrentHealth(int healthValue)
    {
        CurrentHealth += healthValue;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
    }

    public void MaxCurrentHealth()
    {
        CurrentHealth = _maxHealth;
    }

    public void SetIsAlive(bool isAlive)
    {
        IsAlive = isAlive;
    }

    public void SetInvicibilityDuration(float invicibilityDurationSeconds)
    {
        _invicibilityDurationSeconds = invicibilityDurationSeconds;
    }

    public IEnumerator StartInvincibilty()
    {
        if (_invicibilityDurationSeconds == 0)
        {
            yield break;
        }
        else if (IsAlive && !IsInvicible)
        {
            IsInvicible = true;

            yield return Yielders.WaitForSeconds(_invicibilityDurationSeconds);

            IsInvicible = false;
        }
    }
}

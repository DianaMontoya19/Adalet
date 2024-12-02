using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Adalet/Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header(("Health"))]
    [Range(1, 100)]
    public int MaxHealth = 1;

    [Range(0.0f, 5.0f)]
    public float InvicibilityDurationSeconds = 1;

    [Header(("Movement"))]
    [Range(0.0f, 10.0f)]
    public float MoveSpeed = 0.0f;

    [Header(("Damage"))]
    [Range(0, 3)]
    public int CollisionDamage = 1;
}

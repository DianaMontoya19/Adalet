using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Adalet/Projectile/Projectile Data")]
public class ProjectileData : ScriptableObject
{
    [Range(1, 10)]
    public int Damage = 1;

    [Range(0.01f, 10.0f)]
    public float Speed = 0.01f;

    [Range(0.0f, 1.0f)]
    public float ShotDelay = 0.0f;

    [Range(0.0f, 10.0f)]
    public float DestroyTime = 0.0f;
}

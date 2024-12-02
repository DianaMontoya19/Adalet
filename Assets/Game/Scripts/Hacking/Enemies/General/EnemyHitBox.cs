using UnityEngine;

public class EnemyHitBox : HitBox
{
    private EnemyData _data;

    private void Start()
    {
        _data = GetComponentInParent<Enemy>().Data;
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        if (collider.CompareTag(TagStrings.Player))
        {
            PlayerHacking player = collider.GetComponentInParent<PlayerHacking>();

            player.Damage(_data.CollisionDamage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Slime : BaseEnemy
{
    [SerializeField]
    private Slime _smallSlimePrefab;

    public override void Attack(Player player)
    {
        Debug.Log("Attacking the player!");
        player.Hurt(Damage);
    }

    protected override void Dead()
    {
        if (_smallSlimePrefab != null)
        {
            Instantiate(_smallSlimePrefab, transform.position, Quaternion.identity);
            Instantiate(_smallSlimePrefab, transform.position, Quaternion.identity);
        }

        base.Dead();
    }
}

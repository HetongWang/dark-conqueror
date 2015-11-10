using UnityEngine;
using System.Collections;

public class EnemyCommonAttack : BasicAttack
{
    public override void Awake()
    {
        base.Awake();
        targetTag.Add("Player");
        Destroy(gameObject, setting.attackDuration);
    }
}

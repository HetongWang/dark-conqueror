using UnityEngine;
using System.Collections.Generic;

class RotopollyAttack : EnemyCommonAttack
{
    public override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        setting.damage = owner.body.velocity.x * RotopollySet.Instance.run.damage;
        Rotopolly r = (Rotopolly)owner;
        if (!r.couldRun)
        {
            Destroy(gameObject);
        }
    }
}

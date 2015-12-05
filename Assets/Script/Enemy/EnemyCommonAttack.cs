using UnityEngine;
using System.Collections;

public class EnemyCommonAttack : BasicAttack
{
    public override void Awake()
    {
        base.Awake();
        targetTag.Add("Player");
    }
    
    void Start()
    {
        if (setting.attackDuration > 0 && !float.IsInfinity(setting.attackDuration))
            Destroy(gameObject, setting.attackDuration);
    }
}

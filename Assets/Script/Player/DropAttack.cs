using UnityEngine;
using System.Collections;

public class DropAttack : PCCommonAttack
{
    protected override void Start()
    {
        base.Start();
        PointEffector2D newton = GetComponent<PointEffector2D>();
        newton.forceMagnitude = PlayerSet.Instance.dropAttackForce;
    }
}

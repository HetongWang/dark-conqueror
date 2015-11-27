using UnityEngine;
using System.Collections;

public class KittyThrustAttack : BasicAttack {

    public override void Awake()
    {
        base.Awake();
        targetTag.Add("Player");
    }

    void Start()
    {
        Destroy(gameObject, setting.attackDuration);
    }

    public override void getDemage(Collider2D col)
    {
        base.getDemage(col);
    }
}

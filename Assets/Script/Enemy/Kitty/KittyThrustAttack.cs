using UnityEngine;
using System.Collections;

public class KittyThrustAttack : BasicAttack {

    public override void Awake()
    {
        base.Awake();
        setAttr(KittySet.KittyThrust.clone());
        targetTag.Add("Player");
        Destroy(gameObject, setting.attackDuration);
    }

    public override void getDemage(Collider2D col)
    {
        base.getDemage(col);
    }
    public void init(Character c, bool enraged)
    {
        owner = c;
        if (enraged)
        {
            setting.targetForce.x *= KittySet.enrageEnhancement;
            setting.damage *= KittySet.enrageEnhancement;
        }
    }
}

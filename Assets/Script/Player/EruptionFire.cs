using UnityEngine;
using System.Collections;

public class EruptionFire : PCCommonAttack
{
    protected override void Start()
    {
        base.Start();
        deactiveCollider();
    }

    public override void getDemage(Collider2D col)
    {
        base.getDemage(col);
        Character c = col.gameObject.GetComponent<Character>();
        c.statusController.addStatus(new BurnStatus(c, owner.setting.eruptionFireBurn));
    }

    public void activeCollider()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void deactiveCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
}

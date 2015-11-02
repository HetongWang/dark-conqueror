using UnityEngine;
using System.Collections;

public class NormalAttack : BasicAttack {

    protected int phase = 0;

    public override void Awake()
    {
        base.Awake();
        setAttr(PlayerSet.Instance.NormalAttack);
        targetTag.Add("Enemy");
        Destroy(gameObject, duration);
    }

    public override void getDemage(Collider2D col)
    {
        base.getDemage(col);

    }

    public void setPhase(int n)
    {
        phase = n;
    }
}

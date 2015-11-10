﻿using UnityEngine;
using System.Collections;

public class NormalAttack : BasicAttack {

    protected int phase = 0;

    public override void Awake()
    {
        base.Awake();
        setAttr(PlayerSet.Instance.NormalAttack);
        targetTag.Add("Enemy");
        Destroy(gameObject, setting.attackDuration);
    }

    public override void getDemage(Collider2D col)
    {
        base.getDemage(col);
        Character c = col.gameObject.GetComponent<Character>();
        c.statusController.addStatus("burn");
    }

    public void setPhase(int n)
    {
        phase = n;
    }
}

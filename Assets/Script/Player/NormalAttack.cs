﻿using UnityEngine;
using System.Collections;

public class NormalAttack : BasicAttack {

    protected int phase = 0;

    public override void Awake()
    {
        setAttr(PlayerSet.Instance.NormalAttack);
        base.Awake();
        targetTag.Add("Enemy");
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
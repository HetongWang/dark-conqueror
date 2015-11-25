﻿using UnityEngine;
using System.Collections;

public class PCCommonAttack : BasicAttack
{
    protected int level = 1;

    public override void Awake()
    {
        base.Awake();
        targetTag.Add("Enemy");
    }

    protected virtual void Start()
    {
        if (setting.attackDuration != 0)
            Destroy(gameObject, setting.attackDuration);
    }

    public override void getDemage(Collider2D col)
    {
        base.getDemage(col);
    }

    public void setLevel(int level)
    {
        this.level = level;
    }
}

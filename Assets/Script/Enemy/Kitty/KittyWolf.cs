using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KittyWolf : Character
{
    public override void Awake()
    {
        base.Awake();
        moveSpeed = KittySet.Instance.KittyWolfMoveSpeed;
        hp = KittySet.Instance.KittyWolfHP;
    }

    public override void Update()
    {
        base.Update();
        if (facingRight)
            run(1);
        else
            run(-1);
    }

    public void setDirection(bool right)
    {
        facingRight = right;
    }
}
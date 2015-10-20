using UnityEngine;
using System.Collections;

public class KittyThrustAttack : BasicAttack {

    static public float range = 3f;

    public override void Awake()
    {
        base.Awake();
        targetTag = "Player";
        demage = 2;
        duration = 0.6f;
        cd = 2f;
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
    }
}

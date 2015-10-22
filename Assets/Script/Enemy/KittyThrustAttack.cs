using UnityEngine;
using System.Collections;

public class KittyThrustAttack : BasicAttack {

    static public float Range = 1.5f;
    static public float Demage = 1;
    static public float Duration = 0.6f;
    static public float CD = 2;

    public override void Awake()
    {
        _duration = Duration;
        _demage = Demage;
        _cd = CD;
        base.Awake();
        targetTag = "Player";
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
    }
}

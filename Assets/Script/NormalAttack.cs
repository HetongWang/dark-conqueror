using UnityEngine;
using System.Collections;

public class NormalAttack : BasicAttack {
    static public float Ruration = 0.2f;
    static public float CD = 0.5f;

    public override void Awake()
    {
        _duration = Ruration;
        _cd = CD;
        base.Awake();
    }
}

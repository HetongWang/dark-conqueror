using UnityEngine;
using System.Collections;

public class NormalAttack : BasicAttack {
    static public float Ruration = 0.6f;

    public override void Awake()
    {
        _duration = Ruration;
        base.Awake();
    }
}

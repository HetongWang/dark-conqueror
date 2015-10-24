using UnityEngine;
using System.Collections;

public class NormalAttack : BasicAttack {
    static public float Duration = 0.2f;
    static public float CD = 0.5f;
    protected int phase = 0;

    public override void Awake()
    {
        _duration = Duration;
        _cd = CD;
        base.Awake();
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

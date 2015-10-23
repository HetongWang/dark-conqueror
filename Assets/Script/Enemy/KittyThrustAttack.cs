using UnityEngine;
using System.Collections;

public class KittyThrustAttack : BasicAttack {

    static public float Range = 1.5f;
    static public float Demage = 1;
    static public float Duration = 0.6f;
    static public float CD = 2;

    public float forceIntensity = 50f;
    protected Vector2 force;

    public override void Awake()
    {
        _duration = Duration;
        _demage = Demage;
        _cd = CD;
        base.Awake();
        targetTag = "Player";
    }

    public override void getDemage(Collider2D col)
    {
        base.getDemage(col);
        StartCoroutine(attackPhase(col));
    }

    protected IEnumerator attackPhase(Collider2D col)
    {
        yield return new WaitForSeconds(_duration / 2);
        col.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
        yield break;
    }

    public void setForceDriction(Vector2 dirction)
    {
        force = dirction * forceIntensity;
    }
}

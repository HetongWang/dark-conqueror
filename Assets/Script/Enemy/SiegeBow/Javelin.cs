using UnityEngine;
using System.Collections;

public class Javelin : BasicAttack
{
    protected float angle;
    protected Rigidbody2D body;
    protected bool onGround = false;

    public float surviveTime = 4f;

    public override void Awake()
    {
        setAttr(SiegeBowSet.SiegeBowShoot);
        base.Awake();
        body = GetComponent<Rigidbody2D>();
        targetTag.Add("Player");
        targetTag.Add("Ground");
    }

    void Update()
    {
        rotate();
    }

    public override void getDemage(Collider2D col)
    {
        base.getDemage(col);
        if (col.tag == "Ground")
        {
            body.velocity = Vector2.zero;
            body.gravityScale = 0;
            GetComponent<Collider2D>().enabled = false;
            onGround = true;
            Destroy(gameObject, surviveTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void setInitVelocity(Vector2 v)
    {
        body.velocity = v;
    }

    public void rotate()
    {
        if (!onGround)
        {
            angle = Mathf.Atan(body.velocity.y / body.velocity.x);
            angle = angle * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
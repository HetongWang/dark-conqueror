using UnityEngine;
using System.Collections;

public class Javelin : BasicAttack
{
    protected float angle;
    protected Rigidbody2D body;

    public override void Awake()
    {
        setAttr(SkillSetting.Instance.SiegeBowShoot);
        base.Awake();
        body = GetComponent<Rigidbody2D>();
        targetTag = "Player";
    }

    void Update()
    {
        rotate();
    }

    public override void getDemage(Collider2D col)
    {
        base.getDemage(col);
        Destroy(gameObject);
    }

    public void setInitVelocity(Vector2 v)
    {
        body.velocity = v;
    }

    public void rotate()
    {
        angle = Mathf.Atan(body.velocity.y / body.velocity.x);
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
using UnityEngine;

public class KittyWolf : Enemy
{
    protected float direction = -1;
    protected float initPosition;

    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        facingRight = false;
        moveSpeed = KittySet.Instance.KittyWolfMoveSpeed;
        hp = KittySet.Instance.KittyWolfHP;
        initPosition = transform.position.x;

        Destroy(gameObject, 3f);
    }

    public override void Update()
    {
        base.Update();
        detectGround();
        if (Mathf.Abs(transform.position.x - initPosition) > KittySet.Instance.KittyWolfDistance)
        {
            Destroy(gameObject);
        }
    }

    public override void FixedUpdate()
    {
        if (grounded)
        {
            run(direction);
        }

    }

    public void setDirection(float direction)
    {
        this.direction = direction;
    }
}
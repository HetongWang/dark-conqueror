using UnityEngine;

public class KittyWolf : Character
{
    protected float direction = -1;
    protected float initPosition;

    public override void Awake()
    {
        base.Awake();
        facingRight = false;
        moveSpeed = KittySet.Instance.KittyWolfMoveSpeed;
        hp = KittySet.Instance.KittyWolfHP;
        initPosition = transform.position.x;

        Destroy(gameObject, 3f);
    }

    public override void Update()
    {
        base.Update();
        if (grounded)
        {
            run(direction);
        }

        if (Mathf.Abs(transform.position.x - initPosition) > KittySet.Instance.KittyWolfDistance)
        {
            Destroy(gameObject);
        }
    }

    public void setDirection(float direction)
    {
        this.direction = direction;
    }
}
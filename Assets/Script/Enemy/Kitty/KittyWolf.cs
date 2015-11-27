using UnityEngine;

public class KittyWolf : Enemy
{
    protected float direction = -1;
    protected float initPosition;
    protected Kitty owner;

    public override void Awake()
    {
        base.Awake();
        facingRight = false;
        _setting = new CatWolfSet();
        _setting.moveSpeed = owner.setting.KittyWolfMoveSpeed;
        _setting.hp = owner.setting.KittyWolfHP;

        anim = GetComponent<Animator>();
        initPosition = transform.position.x;

        KittyWolfAttack a = transform.FindChild("KittyWolfAttack").GetComponent<KittyWolfAttack>();
        a.init(owner, owner.setting.SummonWolf);
        Destroy(gameObject, 3f);
    }

    public override void Update()
    {
        base.Update();
        detectGround();
        if (Mathf.Abs(transform.position.x - initPosition) > owner.setting.KittyWolfDistance)
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

    public void init(float direction, Kitty owner)
    {
        this.direction = direction;
        this.owner = owner;
    }
}
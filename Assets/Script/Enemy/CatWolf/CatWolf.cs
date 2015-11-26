using UnityEngine;
using System.Collections;

public class CatWolf : Enemy
{
    static public int amount = 0;
    public enum Ability
    {
        idle,
        crouch,
        maul,
        pounce,
        summonFriends,
        alert,
    }

    public GameObject CatWolfAttack;
    public GameObject CatWolfPrefab;

    public bool crouching = false;

    public override void Awake()
    {
        base.Awake();
        facingRight = false;
        moveSpeed = CatWolfSet.Instance.moveSpeed;
        hp = CatWolfSet.Instance.hp;
        anim = GetComponent<Animator>();
        ai = new CatWolfAI(this);
        setHPBar(CatWolfSet.Instance.hpBarOffset, CatWolfSet.Instance.hp);

        addSkill("alert", alert);
        addSkill("maul", maul);
        addSkill("pounce", pounce);
        addSkill("summonFriends", summonFriends, CatWolfSet.Instance.summonFriendsInitCD);
        addSkill("crouch", crouch);
    }

    public override void Start()
    {
        base.Start();
        Vector3 scale = transform.localScale;
        if (scale.x > 0)
            facingRight = false;
        else
            facingRight = true;
    }

    public override void Update()
    {
        base.Update();
        switch (behavior)
        {
            case "alert":
                useSkill(behavior, CatWolfSet.Instance.alert);
                break;
            case "summonFriends":
                if (amount < CatWolfSet.Instance.amount)
                    useSkill(behavior, CatWolfSet.Instance.summonFriends);
                break;
            case "maul":
                useSkill(behavior, CatWolfSet.Instance.maul);
                break;
        }
 
    }

    public override void FixedUpdate()
    {
        if (behavior == "move")
        {
            CatWolfAI _ai = (CatWolfAI)ai;
            _ai.alerted = false;
            run(ai.horMove());
        }
    }

    public override void Hurt(SkillSetting setting, Character source)
    {
        base.Hurt(setting, source);
        if (!invincible)
        {
            CatWolfAI _ai = (CatWolfAI)ai;
            _ai.alerted = false;
        }
    }


    public IEnumerator alert()
    {
        anim.SetInteger("skill", (int)Ability.alert);
        CatWolfAI _ai = (CatWolfAI)ai;
        _ai.alerted = true;
        yield return new WaitForSeconds(CatWolfSet.Instance.alert.actDuration);

        if (behavior == "pounce")
        {
            useSkill(behavior, CatWolfSet.Instance.pounce, false, true);
        }
        else 
            anim.SetInteger("skill", (int)Ability.idle);
        yield break;
    }

    public override void getDemage(float amount)
    {
        if (!invincible)
        {
            if (crouching)
            {
                hp -= amount * CatWolfSet.Instance.crouch.damage;
            }
            else
            {
                hp -= amount;
            }
        }
    }

    public IEnumerator maul()
    {
        anim.SetInteger("skill", (int)Ability.maul);

        Vector3 position = childPosition(new Vector2(0.75f, 0.19f));

        GameObject go = (GameObject)Instantiate(CatWolfAttack, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        go.transform.parent = transform;
        EnemyCommonAttack attack = go.GetComponent<EnemyCommonAttack>();
        attack.init(this, CatWolfSet.Instance.maul);
        yield return new WaitForSeconds(CatWolfSet.Instance.maul.actDuration);

        anim.SetInteger("skill", 0);
        yield break;
    }

    public IEnumerator pounce()
    {
        anim.SetInteger("skill", (int)Ability.pounce);
        yield return new WaitForSeconds(CatWolfSet.Instance.pounce.actDuration * 0.1f);

        addSelfForce(CatWolfSet.Instance.pounce.selfForce);
        Vector3 position = childPosition(new Vector2(0.75f, 0.19f));
        GameObject go = (GameObject)Instantiate(CatWolfAttack, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        go.transform.parent = transform;
        EnemyCommonAttack attack = go.GetComponent<EnemyCommonAttack>();
        attack.init(this, CatWolfSet.Instance.pounce);

        yield return new WaitForSeconds(CatWolfSet.Instance.pounce.actDuration * 0.9f);

        anim.SetInteger("skill", 0);
        CatWolfAI _ai = (CatWolfAI)ai;
        _ai.alerted = false;
        yield break;
    }

    public IEnumerator summonFriends()
    {
        anim.SetInteger("skill", (int)Ability.summonFriends);
        float cameraBorder;
        bool onLeft = Random.value > 0.5f ? true : false;
        if (onLeft)
        {
            cameraBorder = Camera.main.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height;
            cameraBorder -= 2f;
        }
        else
        {
            cameraBorder = Camera.main.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height;
            cameraBorder += 2f;
        }
        Vector3 position = new Vector3(cameraBorder, transform.position.y, transform.position.z);
        Instantiate(CatWolfPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));

        amount += 1;
        yield return new WaitForSeconds(CatWolfSet.Instance.pounce.actDuration);

        anim.SetInteger("skill", 0);
        yield break;
    }

    public IEnumerator crouch()
    {
        anim.SetInteger("skill", (int)Ability.crouch);
        crouching = true;
        yield return new WaitForSeconds(CatWolfSet.Instance.crouch.actDuration);

        crouching = false;
        anim.SetInteger("skill", (int)Ability.idle);
        yield break;
    }
}
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
    public CatWolfSet setting;

    public override void Awake()
    {
        base.Awake();
        facingRight = false;
        _setting = new CatWolfSet();
        setting = (CatWolfSet)_setting;

        addSkill("alert", alert);
        addSkill("maul", maul);
        addSkill("pounce", pounce);
        addSkill("summonFriends", summonFriends, setting.summonFriendsInitCD);
        addSkill("crouch", crouch);

        anim = GetComponent<Animator>();
        ai = new CatWolfAI(this);
        setHPBar(setting.hpBarOffset, setting.hp);
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
                useSkill(behavior, setting.alert, setting.alert.actDuration);
                break;
            case "summonFriends":
                if (amount < CatWolfSet.amount)
                    useSkill(behavior, setting.summonFriends);
                break;
            case "maul":
                useSkill(behavior, setting.maul);
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
        antiStaggerTime = setting.alert.actDuration * 2;
        CatWolfAI _ai = (CatWolfAI)ai;
        _ai.alerted = true;
        yield return new WaitForSeconds(setting.alert.actDuration);

        if (behavior == "pounce")
        {
            useSkill(behavior, setting.pounce, false, true);
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
                hp -= amount * setting.crouch.damage;
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
        attack.init(this, setting.maul);
        yield return new WaitForSeconds(setting.maul.actDuration);

        anim.SetInteger("skill", 0);
        yield break;
    }

    public IEnumerator pounce()
    {
        anim.SetInteger("skill", (int)Ability.pounce);
        yield return new WaitForSeconds(setting.pounce.actDuration * 0.1f);

        addSelfForce(setting.pounce.selfForce);
        Vector3 position = childPosition(new Vector2(0.75f, 0.19f));
        GameObject go = (GameObject)Instantiate(CatWolfAttack, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        go.transform.parent = transform;
        EnemyCommonAttack attack = go.GetComponent<EnemyCommonAttack>();
        attack.init(this, setting.pounce);

        yield return new WaitForSeconds(setting.pounce.actDuration * 0.9f);

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
        yield return new WaitForSeconds(setting.pounce.actDuration);

        anim.SetInteger("skill", 0);
        yield break;
    }

    public IEnumerator crouch()
    {
        anim.SetInteger("skill", (int)Ability.crouch);
        crouching = true;
        yield return new WaitForSeconds(setting.crouch.actDuration);

        crouching = false;
        anim.SetInteger("skill", (int)Ability.idle);
        yield break;
    }
}
using UnityEngine;
using System.Collections;

public class CatWolf : Enemy
{
    public enum Ability
    {
        idle,
        alert,
        maul,
        pounce,
        summonFriends,
        crouch
    }

    public GameObject CatWolfAttack;

    public bool crouching = false;

    public override void Awake()
    {
        base.Awake();
        facingRight = false;
        moveSpeed = CatWolfSet.Instance.moveSpeed;
        hp = CatWolfSet.Instance.hp;
        anim = GetComponent<Animator>();
        setHPBar(CatWolfSet.Instance.hpBarOffset, CatWolfSet.Instance.hp);

        addSkill("alert", alert);
        addSkill("maul", maul);
        addSkill("pounce", pounce);
        addSkill("summonFriends", summonFriends, CatWolfSet.Instance.summonFriendsInitCD);
    }

    public override void Update()
    {
        base.Update();
        switch (behavior)
        {
            case "alert":
                useSkill(behavior, CatWolfSet.Instance.alert);
                break;
            case "maul":
                useSkill(behavior, CatWolfSet.Instance.maul);
                break;
            case "pounce":
                useSkill(behavior, CatWolfSet.Instance.pounce);
                break;
            case "summonFriends":
                useSkill(behavior, CatWolfSet.Instance.summonFriends);
                break;
            case "crouch":
                useSkill(behavior, CatWolfSet.Instance.crouch);
                break;
        }
 
    }

    public override void Hurt(SkillSetting setting)
    {
        if (!invincible)
        {
            if (crouching)
            {
                getDemage(setting.damage * CatWolfSet.Instance.crouch.damage);
            }
            else
            {
                getDemage(setting.damage);
                freezenTime = setting.freezenTime;
                if (anim)
                    anim.SetBool("hurt", true);
            }
        }
    }


    public IEnumerator alert()
    {
        anim.SetInteger("skill", (int)Ability.alert);
        CatWolfAI _ai = (CatWolfAI)ai;
        _ai.alerted = true;
        yield return new WaitForSeconds(CatWolfSet.Instance.alert.actDuration);

        anim.SetInteger("skill", (int)Ability.idle);
        yield break;
    }

    public IEnumerator maul()
    {
        anim.SetInteger("skill", (int)Ability.maul);

        Vector3 position = transform.position;
        position.y = 1f;
        if (facingRight)
        {
            position.x += CatWolfSet.Instance.maul.range / 2;
        }
        else
        {
            position.x -= CatWolfSet.Instance.maul.range / 2;
        }

        GameObject go = (GameObject)Instantiate(CatWolfAttack, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        EnemyCommonAttack attack = go.GetComponent<EnemyCommonAttack>();
        attack.setAttr(CatWolfSet.Instance.maul);
        yield return new WaitForSeconds(CatWolfSet.Instance.maul.actDuration);

        anim.SetInteger("skill", 0);
        CatWolfAI _ai = (CatWolfAI)ai;
        _ai.alerted = false;
        yield break;
    }

    public IEnumerator pounce()
    {
        anim.SetInteger("skill", (int)Ability.pounce);

        Vector3 position = transform.position;
        position.y = 1f;
        if (facingRight)
        {
            position.x += CatWolfSet.Instance.maul.range / 2;
            body.AddForce(CatWolfSet.Instance.pounceForce);
        }
        else
        {
            position.x -= CatWolfSet.Instance.maul.range / 2;
            Vector2 force = new Vector2(-CatWolfSet.Instance.pounceForce.x, CatWolfSet.Instance.pounceForce.y);
            body.AddForce(force);
        }

        GameObject go = (GameObject)Instantiate(CatWolfAttack, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        EnemyCommonAttack attack = go.GetComponent<EnemyCommonAttack>();
        attack.setAttr(CatWolfSet.Instance.pounce);

        yield return new WaitForSeconds(CatWolfSet.Instance.pounce.actDuration);

        anim.SetInteger("skill", 0);
        CatWolfAI _ai = (CatWolfAI)ai;
        _ai.alerted = false;
        yield break;
    }

    public IEnumerator summonFriends()
    {
        anim.SetInteger("skill", (int)Ability.summonFriends);
        float cameraBorder;
        if (Random.value > 0.5f)
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
        Instantiate(gameObject, position, Quaternion.Euler(new Vector3(0, 0, 0)));
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
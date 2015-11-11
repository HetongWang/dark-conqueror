using UnityEngine;
using System.Collections;

public class CatWolf : Enemy
{
    public GameObject CatWolfAttack;

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
        }
 
    }

    public IEnumerator alert()
    {
        anim.SetBool("alert", true);
        CatWolfAI _ai = (CatWolfAI)ai;
        _ai.alerted = true;
        yield return new WaitForSeconds(CatWolfSet.Instance.alert.actDuration);

        anim.SetBool("alert", false);
        yield break;
    }

    public IEnumerator maul()
    {
        anim.SetInteger("attack", 1);

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

        anim.SetInteger("attack", 0);
        CatWolfAI _ai = (CatWolfAI)ai;
        _ai.alerted = false;
        yield break;
    }

    public IEnumerator pounce()
    {
        anim.SetInteger("attack", 2);

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

        anim.SetInteger("attack", 0);
        CatWolfAI _ai = (CatWolfAI)ai;
        _ai.alerted = false;
        yield break;
    }

    public IEnumerator summonFriends()
    {
        anim.SetInteger("attack", 3);
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

        anim.SetInteger("attack", 0);
        yield break;
    }
}
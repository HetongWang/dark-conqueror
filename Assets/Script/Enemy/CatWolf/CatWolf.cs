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

        Destroy(gameObject, 3f);
    }

    public override void Update()
    {
        base.Update();
    }

    public IEnumerator alert()
    {
        anim.SetBool("alert", true);
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
        yield break;
    }

    public IEnumerator pounce()
    {
        anim.SetInteger("attack", 2);
        yield return new WaitForSeconds(CatWolfSet.Instance.pounce.actDuration);

        anim.SetInteger("attack", 0);
        yield break;
    }

    public IEnumerator summonFriends()
    {
        anim.SetInteger("attack", 3);
        yield return new WaitForSeconds(CatWolfSet.Instance.pounce.actDuration);

        anim.SetInteger("attack", 0);
        yield break;
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wasp : Enemy
{
    public GameObject attackPrefab;
    [HideInInspector]
    public WaspSet setting;
    private Vector3 velocity;

    public override void Awake()
    {
        base.Awake();
        _setting = new WaspSet();
        setting = (WaspSet)_setting;
        setting.highHeight = transform.position.y;
        body.gravityScale = 0;
        facingRight = false;
        behavior = "high";

        ai = new WaspAI(this);
        anim = GetComponent<Animator>();
        setHPBar(setting.hpBarOffset, setting.hp);

        addSkill("high", inHigh);
        addSkill("low", inLow);
        addSkill("attack", attack);
    }

    public override void Update()
    {
        base.Update();
        if (died)
            return;

        ai.faceToPlayer();
        switch (behavior)
        {
            case "high":
                useSkill("high", setting.high);
                break;
            case "low":
                useSkill("low", setting.low);
                break;
            case "attack":
                useSkill("attack", setting.attack);
                break;
        }
    }

    public IEnumerator inHigh()
    {
        while (true)
        {
            if (transform.position.y < setting.highHeight - 0.1f)
            {
                move(0, 1);
                yield return new WaitForEndOfFrame();
            }
            else
            {
                move(0, 0);
                break;
            }
        }
        yield break;
    }

    public IEnumerator inLow()
    {
        while (true)
        {
            if (transform.position.y > setting.highHeight - setting.refLowHeight)
            {
                move(0, -1);
                yield return new WaitForEndOfFrame();
            }
            else
            {
                move(0, 0);
                break;
            }
        }
        yield break;
    }


    public IEnumerator attack()
    {
        float dis = 2f;
        float timer = 0;
        // actDuration is infinity

        Vector3 newPosition;
        if (facingRight)
            newPosition = new Vector3(transform.position.x - dis, transform.position.y, 0);
        else
            newPosition = new Vector3(transform.position.x + dis, transform.position.y, 0);
        newPosition.y += 1f;

        while (timer < 1f && transform.position != newPosition)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                                 Quaternion.Euler(new Vector3(0, 0, facingRight ? -45 : 45)), 90 * Time.deltaTime);
            hpBar.transform.localRotation = Quaternion.RotateTowards(transform.rotation,
                                            Quaternion.Euler(new Vector3(0, 0, facingRight ? -45 : 45)), 90 * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, newPosition, setting.attackMoveSpeed * Time.deltaTime / 2);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        // wasp prepare attack

        GameObject attackObject = Instantiate(attackPrefab);
        attackObject.transform.parent = transform;
        attackObject.transform.localPosition = new Vector2(-0.156f, -1.4f);
        attackObject.transform.localScale = new Vector3(1.25f, 1.25f, 1);
        BasicAttack waspAttack = attackObject.GetComponent<EnemyCommonAttack>();
        waspAttack.init(this, setting.attack);

        Vector3 playerPosition = ai.targetPlayer.transform.position;
        Vector3 endPosition = transform.position;
        endPosition.y = setting.highHeight - setting.refLowHeight;
        if (facingRight)
            endPosition.x += ai.targetPlayerDistance * 1.5f;
        else
            endPosition.x -= ai.targetPlayerDistance * 1.5f;
        while (transform.position != playerPosition)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                                 Quaternion.Euler(new Vector3(0, 0, facingRight ? 45 : -45)), 270 * Time.deltaTime);
            hpBar.transform.localRotation = Quaternion.RotateTowards(transform.rotation,
                                            Quaternion.Euler(new Vector3(0, 0, facingRight ? 45 : -45)), 90 * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, setting.attackMoveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        // complete attack

        while (transform.position != endPosition)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, 
                                 Quaternion.Euler(new Vector3(0, 0, 0)), 270 * Time.deltaTime);
            hpBar.transform.localRotation = Quaternion.RotateTowards(transform.rotation,
                                            Quaternion.Euler(new Vector3(0, 0, 0)), 90 * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, endPosition, setting.attackMoveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        // wasp return low

        // Attack completed
        Destroy(attackObject);
        actingTime = 0;
        anim.SetInteger("skill", 0);
        yield break;
    }

    public override IEnumerator dying()
    {
        if (anim)
            anim.SetBool("dying", true);
        body.gravityScale = 1;
        StartCoroutine(soulsExplosion());
        while (!grounded)
        {
            yield return new WaitForEndOfFrame();
        }

        anim.speed = 0;
        Destroy(hpBar);
        yield return new WaitForSeconds(disappearTime);
        Destroy(gameObject);
        yield break;
    }
}

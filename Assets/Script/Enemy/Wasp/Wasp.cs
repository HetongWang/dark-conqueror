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

        behavior = "high";
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
        if (transform.position.y < setting.highHeight)
        {
            move(0, 1);
        }
        else
        {
            move(0, 0);
            farting();
        }
        yield break;
    }

    public IEnumerator inLow()
    {
        if (transform.position.y > setting.highHeight  - setting.refLowHeight)
        {
            move(0, -1);
        }
        else
        {
            move(0, 0);
            farting();
        }
        yield break;
    }

    public void farting()
    {
        float angle = Random.value * 360 * Mathf.Deg2Rad;
        Vector3 position = new Vector3(Mathf.Cos(angle) * setting.fartingRadius, Mathf.Sin(angle) * setting.fartingRadius, 0);
        transform.position = Vector3.SmoothDamp(transform.position, position, ref velocity, 0.5f);
    }

    public IEnumerator attack()
    {
        float dis = 2f;
        float timer = 0;
        Vector3 newPosition;
        if (facingRight)
            newPosition = new Vector3(transform.position.x - dis, transform.position.y, 0);
        else
            newPosition = new Vector3(transform.position.x + dis, transform.position.y, 0);

        while (timer < 1f || transform.position != newPosition)
        {
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, 0.2f, setting.attackMoveSpeed);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }


        GameObject attackObject = Instantiate(attackPrefab);
        attackObject.transform.parent = transform;
        attackObject.transform.localPosition = new Vector2(-0.156f, -1.4f);
        attackObject.transform.localScale = new Vector3(1.25f, 1.25f, 1);
        BasicAttack waspAttack = attackObject.GetComponent<EnemyCommonAttack>();
        waspAttack.init(this, setting.attack);

        Vector3 playerPosition = ai.targetPlayer.transform.position;
        Vector3 endPosition = transform.position;
        if (facingRight)
            endPosition.x += ai.targetPlayerDistance * 2;
        else
            endPosition.x -= ai.targetPlayerDistance * 2;
        while (transform.position != playerPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, setting.attackMoveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        while (transform.position != endPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, setting.attackMoveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        // Attack completed
        Destroy(attackObject);
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

using UnityEngine;
using System.Collections;

public class Rotopolly : Enemy
{
    public GameObject attackPrefab;
    public enum Ability
    {
        idle,
        run,
        jump
    }

    [HideInInspector]
    public bool couldRun = false;

    public override void Awake()
    {
        base.Awake();
        hp = RotopollySet.Instance.hp;
        moveSpeed = RotopollySet.Instance.moveSpeed;

        facingRight = false;
        ai = new RotopollyAI(this);
        anim = GetComponent<Animator>();
        setHPBar(RotopollySet.Instance.hpBarOffset, RotopollySet.Instance.hp);

        addSkill("run", run);
        addSkill("jump", jump);
    }

    public override void Update()
    {
        base.Update();
        running();
        behavior = "run";
        switch (behavior)
        {
            case "run":
                if (!couldRun)
                    useSkill("run", RotopollySet.Instance.run);
                break;
            case "jump":
                useSkill("jump", RotopollySet.Instance.jump);
                break;
            case "stopRun":
                stopRun();
                break;
        }
    }

    public void running()
    {
        if (behavior == "run" && couldRun)
        {
            if (Mathf.Abs(body.velocity.x) < RotopollySet.Instance.runSpeed)
            {
                float x;
                if (facingRight)
                    x = RotopollySet.Instance.runAcceleration * Time.deltaTime + body.velocity.x;
                else
                    x = body.velocity.x - RotopollySet.Instance.runAcceleration * Time.deltaTime;

                Vector2 v = new Vector2(x, body.velocity.y);
                body.velocity = v;
            }
            anim.speed = Mathf.Abs(body.velocity.x / RotopollySet.Instance.moveSpeed);
        }
    }

    public void stopRun()
    {
        anim.speed = 1;
        couldRun = false;
        StopCoroutine(currentSkill);
        anim.SetInteger("skill", 0);
    }

    public IEnumerator run()
    {
        Vector3 position = childPosition(new Vector2(0, 0));
        GameObject go = (GameObject)Instantiate(attackPrefab, position, Quaternion.Euler(0, 0, 0));
        RotopollyAttack a = go.GetComponent<RotopollyAttack>();
        a.init(this, RotopollySet.Instance.run);

        anim.SetInteger("skill", 1);
        yield return new WaitForSeconds(RotopollySet.Instance.run.actDuration);

        couldRun = true;
        anim.SetInteger("skill", 1);
        yield break;
    }

    public IEnumerator jump()
    {
        if (grounded)
            body.AddForce(RotopollySet.Instance.jump.selfForce);
        yield break;
    }
}
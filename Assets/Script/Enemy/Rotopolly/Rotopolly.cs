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
    public GameObject attackObject = null;

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
        StartCoroutine(running());
        switch (behavior)
        {
            case "run":
                if (!couldRun)
                    useSkill("run", RotopollySet.Instance.run);
                break;
            case "jump":
                useSkill("jump", RotopollySet.Instance.jump);
                break;
            case "idle":
                stopRun();
                break;
        }
    }

    public IEnumerator running()
    {
        if (behavior == "run" && couldRun)
        {
            if (Mathf.Abs(body.velocity.x) < RotopollySet.Instance.runSpeed)
            {
                Vector2 v = new Vector2(0, body.velocity.y);
                if (facingRight)
                    v.x = RotopollySet.Instance.runAcceleration * Time.deltaTime + body.velocity.x;
                else
                    v.x = body.velocity.x - RotopollySet.Instance.runAcceleration * Time.deltaTime;

                anim.speed = Mathf.Abs(body.velocity.x / RotopollySet.Instance.moveSpeed);
                yield return new WaitForFixedUpdate();
                body.velocity = v;
            }
        }
        yield break;
    }

    public void stopRun()
    {
        anim.speed = 1;
        couldRun = false;
        if (currentSkill != null)
            StopCoroutine(currentSkill);
        if (attackObject != null)
            Destroy(attackObject);
        anim.SetInteger("skill", 0);
    }

    public IEnumerator run()
    {
        anim.SetInteger("skill", 1);
        yield return new WaitForSeconds(RotopollySet.Instance.run.actDuration);

        couldRun = true;
        Vector3 position = childPosition(new Vector2(0, 0));
        attackObject = (GameObject)Instantiate(attackPrefab, position, Quaternion.Euler(0, 0, 0));
        attackObject.transform.parent = transform;
        RotopollyAttack a = attackObject.GetComponent<RotopollyAttack>();
        a.init(this, RotopollySet.Instance.run);
        anim.SetInteger("skill", 2);
        yield break;
    }

    public IEnumerator jump()
    {
        if (grounded)
            body.AddForce(RotopollySet.Instance.jump.selfForce);
        yield break;
    }
}
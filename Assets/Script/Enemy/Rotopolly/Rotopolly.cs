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
    public RotopollySet setting;

    public override void Awake()
    {
        base.Awake();
        _setting = new RotopollySet();
        setting = (RotopollySet)_setting;

        facingRight = false;
        ai = new RotopollyAI(this);
        anim = GetComponent<Animator>();
        setHPBar(setting.hpBarOffset, setting.hp);

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
                    useSkill("run", setting.run);
                break;
            case "jump":
                useSkill("jump", setting.jump);
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
            if (Mathf.Abs(body.velocity.x) < setting.runSpeed)
            {
                Vector2 v = new Vector2(0, body.velocity.y);
                if (facingRight)
                    v.x = setting.runAcceleration * Time.deltaTime + body.velocity.x;
                else
                    v.x = body.velocity.x - setting.runAcceleration * Time.deltaTime;

                anim.speed = Mathf.Abs(body.velocity.x / setting.moveSpeed);
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

    public override IEnumerator dying()
    {
        stopRun();
        return base.dying();
    }

    public void newAttack()
    {
        Vector3 position = childPosition(new Vector2(0, 0));
        attackObject = (GameObject)Instantiate(attackPrefab, position, Quaternion.Euler(0, 0, 0));
        attackObject.transform.parent = transform;
        RotopollyAttack a = attackObject.GetComponent<RotopollyAttack>();
        a.init(this, setting.run);
    }

    public IEnumerator run()
    {
        anim.SetInteger("skill", 1);
        yield return new WaitForSeconds(setting.run.actDuration);

        couldRun = true;
        newAttack();
        anim.SetInteger("skill", 2);
        yield break;
    }

    public IEnumerator jump()
    {
        if (grounded)
            body.AddForce(setting.jump.selfForce);
        yield break;
    }
}
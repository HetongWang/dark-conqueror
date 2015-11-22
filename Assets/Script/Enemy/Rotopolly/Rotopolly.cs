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

    public override void Awake()
    {
        base.Awake();
        hp = RotopollySet.Instance.hp;
        moveSpeed = RotopollySet.Instance.moveSpeed;

        facingRight = false;
        ai = new RotopollyAI(this);
        anim = GetComponent<Animator>();
        setHPBar(RotopollySet.Instance.hpBarOffset, RotopollySet.Instance.hp);
    }

    public override void Update()
    {
        base.Update();
        running();
        switch (behavior)
        {
            case "run":
                useSkill("run", RotopollySet.Instance.run);
                break;
            case "jump":
                useSkill("jump", RotopollySet.Instance.jump);
                break;
        }
    }

    public void running()
    {
        if (behavior == "run")
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
        else
        {
            anim.speed = 1;
        }
    }

    public IEnumerator run()
    {
        Vector3 position = childPosition(new Vector2(0, 0));
        GameObject go = (GameObject)Instantiate(attackPrefab, position, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(RotopollySet.Instance.run.actDuration);
    }

    public IEnumerator jump()
    {
        yield break;
    }
}
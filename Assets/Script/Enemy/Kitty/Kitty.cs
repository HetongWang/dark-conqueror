using UnityEngine;
using System.Collections;

public class Kitty: Enemy
{
    public enum Ability
    {
        idle,
        thrust,
        summonWolf,
        leap,
        slash
    }

    public GameObject thrustAttackPrefab;
    public GameObject enragePrefab;
    public GameObject kittyWolfPrefab;

    public bool enraged = false;

    public override void Awake()
    {
        base.Awake();
        addSkill("thrust", thrustAttack);
        addSkill("enrage", enrage);
        addSkill("summonWolf", summonWolf);
        addSkill("leap", leap);

        facingRight = false;
        hp = KittySet.Instance.hp;

        ai = new KittyAI(this);
        anim = GetComponent<Animator>();
        setHPBar(KittySet.Instance.hpBarOffset, KittySet.Instance.hp);
    }

    public override void Update()
    {
        base.Update();
        behavior = "leap";
        switch (behavior)
        {
            case "enrage":
                useSkill(behavior, KittySet.Instance.KittyEnrage, false, true);
                break;
            case "summonWolf":
                useSkill(behavior, KittySet.Instance.SummonWolf);
                break;
            case "thrust":
                useSkill(behavior, KittySet.Instance.KittyThrust);
                break;
            case "leap":
                useSkill(behavior, KittySet.Instance.Leap);
                break;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public IEnumerator thrustAttack()
    {
        Vector3 position = transform.position;
        position.y += 0.2f;
        anim.SetInteger("skill", (int)Ability.thrust);

        if (facingRight)
        { 
            position.x += KittySet.Instance.KittyThrust.range / 2;
        }
        else
        {
            position.x -= KittySet.Instance.KittyThrust.range / 2;
        }

        GameObject gameo = (GameObject)Instantiate(thrustAttackPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        KittyThrustAttack thrust = gameo.GetComponent<KittyThrustAttack>();
        thrust.init(facingRight ? Vector2.right : Vector2.left, enraged);

        yield return new WaitForSeconds(KittySet.Instance.KittyThrust.actDuration);

        anim.SetInteger("skill", 0);
        yield break;
    }

    public IEnumerator enrage()
    {
        enraged = true;
        moveSpeed *= KittySet.Instance.enrageEnhancement;
        Instantiate(enragePrefab, transform.position, Quaternion.Euler(Vector3.zero));
        anim.SetBool("enrage", true);
        yield return new WaitForSeconds(KittySet.Instance.KittyEnrage.actDuration);

        anim.SetBool("enrage", false);
        Vector3 scale = transform.localScale;
        scale *= 1.1f;
        transform.localScale = scale;
        yield break;
    }

    public IEnumerator summonWolf()
    {
        anim.SetInteger("skill", (int)Ability.summonWolf);

        // First wolf
        Vector3 position = transform.position;
        position.y += 10;
        GameObject g1 = (GameObject)Instantiate(kittyWolfPrefab, position, Quaternion.Euler(0, 0, 0));
        KittyWolf w1 = g1.GetComponent<KittyWolf>();
        w1.setDirection(facingRight ? 1 : -1);

        // Second wolf
        position.x = ai.targetPlayer.transform.position.x;
        if (facingRight)
            position.x += 5f;
        else
            position.x -= 5f;
        GameObject g2 = (GameObject)Instantiate(kittyWolfPrefab, position, Quaternion.Euler(0, 0, 0));
        KittyWolf w2 = g2.GetComponent<KittyWolf>();
        w2.setDirection(!facingRight ? 1 : -1);

        yield return new WaitForSeconds(KittySet.Instance.SummonWolf.actDuration);

        anim.SetInteger("skill", 0);
        yield break;
    }

    public IEnumerator leap()
    {
        float timer = Time.time;
        anim.SetInteger("skill", (int)Ability.leap);
        yield return new WaitForSeconds(7 / 24);

        body.velocity = leapVelocity();
        yield return new WaitForSeconds(KittySet.Instance.Leap.actDuration - 7 / 24);

        Debug.Log(Time.time - timer);
        anim.SetInteger("skill", 0);
        yield break;
    }

    private Vector2 leapVelocity()
    {
        float h = 0;
        float d = KittySet.Instance.Leap.range;
        float g = Physics2D.gravity.y;
        float rad = KittySet.Instance.LeapAngle * Mathf.Deg2Rad;

        float vx = -d * Mathf.Sqrt(g / (2 * (h - Mathf.Tan(rad) * d)));
        float vy = Mathf.Abs(Mathf.Tan(rad) * vx);
        Vector2 v = new Vector2(vx, vy);

        if (!facingRight)
            v.x = Mathf.Abs(v.x);
        else
            v.x = -Mathf.Abs(v.x);
        return v;
    }

    public IEnumerator slash()
    {
        yield break;
    }
}
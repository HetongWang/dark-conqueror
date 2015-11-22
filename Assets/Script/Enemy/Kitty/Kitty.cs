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
        slash,
        enrage
    }

    public GameObject thrustAttackPrefab;
    public GameObject slash1Prefab;
    public GameObject slash2Prefab;
    public GameObject enragePrefab;
    public GameObject kittyWolfPrefab;

    public bool enraged = false;
    private bool slashing = false;

    public override void Awake()
    {
        base.Awake();
        addSkill("thrust", thrustAttack);
        addSkill("enrage", enrage);
        addSkill("summonWolf", summonWolf);
        addSkill("leap", leap);
        addSkill("slash", slash);

        facingRight = false;
        hp = KittySet.Instance.hp;

        ai = new KittyAI(this);
        anim = GetComponent<Animator>();
        setHPBar(KittySet.Instance.hpBarOffset, KittySet.Instance.hp);
    }

    public override void Update()
    {
        base.Update();
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
            case "slash":
                useSkill(behavior, KittySet.Instance.Slash);
                break;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        slashingUpdate();
    }

    protected void slashingUpdate()
    {
        if (slashing)
        {
            Vector2 v;
            if (facingRight)
                v = new Vector2(KittySet.Instance.slashMoveDist / KittySet.Instance.Slash.actDuration, 0);
            else 
                v = new Vector2(- KittySet.Instance.slashMoveDist / KittySet.Instance.Slash.actDuration, 0);
            body.velocity = v;
        }
    }

    public override void Hurt(SkillSetting setting, Character source)
    {
        base.Hurt(setting, source);
        if (!invincible && !blocked)
            slashing = false;
    }

    public override IEnumerator dying()
    {
        Destroy(hpBar);
        anim.SetBool("dying", true);
        anim.speed = 0.5f;
        yield break;
    }

    public IEnumerator thrustAttack()
    {
        anim.SetInteger("skill", (int)Ability.thrust);
        Vector3 position = childPosition(new Vector2(KittySet.Instance.KittyThrust.range / 2, 0.2f));

        GameObject gameo = (GameObject)Instantiate(thrustAttackPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        KittyThrustAttack thrust = gameo.GetComponent<KittyThrustAttack>();
        thrust.init(this, enraged);
        thrust.transform.parent = transform;

        yield return new WaitForSeconds(KittySet.Instance.KittyThrust.actDuration);

        anim.SetInteger("skill", 0);
        yield break;
    }

    public IEnumerator enrage()
    {
        enraged = true;
        if (currentSkill != null)
            StopCoroutine(currentSkill);
        moveSpeed *= KittySet.Instance.enrageEnhancement;
        Instantiate(enragePrefab, transform.position, Quaternion.Euler(Vector3.zero));
        anim.SetInteger("skill", (int)Ability.enrage);
        anim.SetBool("enrage", true);
        yield return new WaitForSeconds(KittySet.Instance.KittyEnrage.actDuration);

        anim.SetInteger("skill", (int)Ability.idle);
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
        anim.SetInteger("skill", (int)Ability.leap);
        yield return new WaitForSeconds(9 / 24);

        body.velocity = leapVelocity();
        yield return new WaitForSeconds(KittySet.Instance.Leap.actDuration - 9 / 24);

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
        anim.SetInteger("skill", (int)Ability.slash);
        yield return new WaitForSeconds(KittySet.Instance.Slash.actDuration * 0.15f);

        slashing = true;
        yield return new WaitForSeconds(KittySet.Instance.Slash.actDuration * 0.10f);

        Vector3 position = childPosition(new Vector2(0.72f, 0f));
        GameObject go1 = (GameObject)Instantiate(slash1Prefab, position, Quaternion.Euler(0, 0, 0));
        go1.transform.parent = transform;
        KittySlash slash1 = go1.GetComponent<KittySlash>();
        slash1.init(this, enraged);
        yield return new WaitForSeconds(KittySet.Instance.Slash.actDuration * 0.35f);

        position = childPosition(new Vector2(0.57f, 0f));
        GameObject go2 = (GameObject)Instantiate(slash2Prefab, position, Quaternion.Euler(0, 0, 0));
        go2.transform.parent = transform;
        KittySlash slash2 = go2.GetComponent<KittySlash>();
        slash2.init(this, enraged);
        slashing = false;
        yield return new WaitForSeconds(KittySet.Instance.Slash.actDuration * 0.4f);

        anim.SetInteger("skill", (int)Ability.idle);
        yield break;
    }
}
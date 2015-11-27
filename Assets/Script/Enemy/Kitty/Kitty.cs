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
    [HideInInspector]
    public KittySet setting;

    public override void Awake()
    {
        base.Awake();
        _setting = new KittySet();
        setting = (KittySet)_setting;
        addSkill("thrust", thrustAttack);
        addSkill("enrage", enrage);
        addSkill("summonWolf", summonWolf);
        addSkill("leap", leap);
        addSkill("slash", slash);

        facingRight = false;

        ai = new KittyAI(this);
        anim = GetComponent<Animator>();
        setHPBar(setting.hpBarOffset, setting.hp);
    }

    public override void Update()
    {
        base.Update();
        switch (behavior)
        {
            case "enrage":
                useSkill(behavior, setting.KittyEnrage, false, true);
                break;
            case "summonWolf":
                useSkill(behavior, setting.SummonWolf);
                break;
            case "thrust":
                useSkill(behavior, setting.KittyThrust);
                break;
            case "leap":
                useSkill(behavior, setting.Leap);
                break;
            case "slash":
                useSkill(behavior, setting.slash);
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
                v = new Vector2(setting.slashMoveDist / setting.slash.actDuration, 0);
            else 
                v = new Vector2(- setting.slashMoveDist / setting.slash.actDuration, 0);
            body.velocity = v;
        }
    }

    public override void Hurt(SkillSetting setting, Character source)
    {
        base.Hurt(setting, source);
        if (!invincible && !blocked)
            slashing = false;
        if (hp <= 0)
            StartCoroutine(GameManager.slowMotion(0.2f, 0.2f, 3f));
    }

    public override IEnumerator dying()
    {
        Destroy(hpBar);
        anim.SetBool("dying", true);
        yield break;
    }

    public IEnumerator thrustAttack()
    {
        anim.SetInteger("skill", (int)Ability.thrust);
        yield return new WaitForSeconds(setting.KittyThrust.actDuration / 2);

        Vector3 position = childPosition(new Vector2(setting.KittyThrust.range / 2, 0.2f));
        GameObject gameo = (GameObject)Instantiate(thrustAttackPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        KittyThrustAttack thrust = gameo.GetComponent<KittyThrustAttack>();
        thrust.init(this, setting.KittyThrust);
        thrust.transform.parent = transform;
        yield return new WaitForSeconds(setting.KittyThrust.actDuration / 2);

        anim.SetInteger("skill", 0);
        yield break;
    }

    public IEnumerator enrage()
    {
        enraged = true;
        if (currentSkill != null)
            cancelCurrentSkill();
        enrageEnhance();
        Instantiate(enragePrefab, transform.position, Quaternion.Euler(Vector3.zero));
        anim.SetInteger("skill", (int)Ability.enrage);
        anim.SetBool("enrage", true);
        yield return new WaitForSeconds(setting.KittyEnrage.actDuration);

        anim.SetInteger("skill", (int)Ability.idle);
        anim.SetBool("enrage", false);
        Vector3 scale = transform.localScale;
        scale *= 1.1f;
        transform.localScale = scale;
        yield break;
    }

    private void enrageEnhance()
    {
        setting.moveSpeed *= setting.enrageEnhancement;
        setting.slash.damage *= setting.enrageEnhancement;
        setting.slash.cd *= setting.enrageEnhancement;
        setting.KittyThrust.damage *= setting.enrageEnhancement;
        setting.KittyThrust.targetForce *= setting.enrageEnhancement;
    }

    public IEnumerator summonWolf()
    {
        anim.SetInteger("skill", (int)Ability.summonWolf);

        // First wolf
        Vector3 position = transform.position;
        position.y += 10;
        GameObject g1 = (GameObject)Instantiate(kittyWolfPrefab, position, Quaternion.Euler(0, 0, 0));
        KittyWolf w1 = g1.GetComponent<KittyWolf>();
        w1.init(facingRight ? 1 : -1, this);

        // Second wolf
        position.x = ai.targetPlayer.transform.position.x;
        if (facingRight)
            position.x += 5f;
        else
            position.x -= 5f;
        GameObject g2 = (GameObject)Instantiate(kittyWolfPrefab, position, Quaternion.Euler(0, 0, 0));
        KittyWolf w2 = g2.GetComponent<KittyWolf>();
        w2.init(!facingRight ? 1 : -1, this);

        yield return new WaitForSeconds(setting.SummonWolf.actDuration);

        anim.SetInteger("skill", 0);
        yield break;
    }

    public IEnumerator leap()
    {
        anim.SetInteger("skill", (int)Ability.leap);
        yield return new WaitForSeconds(9 / 24);

        body.velocity = leapVelocity();
        yield return new WaitForSeconds(setting.Leap.actDuration - 9 / 24);

        anim.SetInteger("skill", 0);
        yield break;
    }

    private Vector2 leapVelocity()
    {
        float h = 0;
        float d = setting.Leap.range;
        float g = Physics2D.gravity.y;
        float rad = setting.LeapAngle * Mathf.Deg2Rad;

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
        antiStaggerTime = setting.slash.actDuration;
        anim.SetInteger("skill", (int)Ability.slash);
        yield return new WaitForSeconds(setting.slash.actDuration * 0.15f);

        slashing = true;
        yield return new WaitForSeconds(setting.slash.actDuration * 0.10f);

        Vector3 position = childPosition(new Vector2(0.72f, 0f));
        GameObject go1 = (GameObject)Instantiate(slash1Prefab, position, Quaternion.Euler(0, 0, 0));
        go1.transform.parent = transform;
        KittySlash slash1 = go1.GetComponent<KittySlash>();
        slash1.init(this, setting.slash);
        yield return new WaitForSeconds(setting.slash.actDuration * 0.35f);

        position = childPosition(new Vector2(0.57f, 0f));
        GameObject go2 = (GameObject)Instantiate(slash2Prefab, position, Quaternion.Euler(0, 0, 0));
        go2.transform.parent = transform;
        KittySlash slash2 = go2.GetComponent<KittySlash>();
        slash2.init(this, setting.slash);
        slashing = false;
        yield return new WaitForSeconds(setting.slash.actDuration * 0.4f);

        anim.SetInteger("skill", (int)Ability.idle);
        yield break;
    }
}
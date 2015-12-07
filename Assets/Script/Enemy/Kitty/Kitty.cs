using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Kitty: Enemy
{
    public enum Ability
    {
        idle,
        thrust,
        summonWolf,
        leap,
        slash,
        enrage,
        shadowFadeOut,
        shadowAttack,
        shadowFadeIn
    }

    public GameObject thrustAttackPrefab;
    public GameObject slash1Prefab;
    public GameObject slash2Prefab;
    public GameObject enragePrefab;
    public GameObject kittyWolfPrefab;
    public GameObject shadowAttackPrefab;

    public bool enraged = false;
    private bool slashing = false;
    private bool shadowAttacking = false;
    [HideInInspector]
    public KittySet setting;

    public override void Awake()
    {
        base.Awake();
        _setting = new KittySet();
        dyingDuration = 1.5f;
        setting = (KittySet)_setting;
        addSkill("idle", idle);
        addSkill("thrust", thrustAttack);
        addSkill("enrage", enrage);
        addSkill("summonWolf", summonWolf);
        addSkill("leap", leap);
        addSkill("slash", slash);
        addSkill("shadow", shadow);

        facingRight = false;

        ai = new KittyAI(this);
        anim = GetComponent<Animator>();
    }

    public override void Start()
    {
        base.Start();
        GameManager.Instance.activeBossHPBar(this);
    }

    public override void Update()
    {
        if (shadowAttacking)
            return;
        base.Update();
        switch (behavior)
        {
            case "idle":
                setting.idle.actDuration = setting.idleTime.random();
                useSkill(behavior, setting.idle);
                break;
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
            case "shadow":
                useSkill(behavior, setting.shadowAttack, true);
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
    }

    public override IEnumerator dying()
    {
        Destroy(hpBar);
        StartCoroutine(GameManager.slowMotion(0.2f, 0.2f, 3f));
        StartCoroutine(soulsExplosion());
        anim.SetBool("dying", true);

        yield return new WaitForSeconds(dyingDuration);
        List<Dialog> conver = new List<Dialog>();
        conver.Add(new Dialog("Kitty", "Kill... kill you..."));
        conver.Add(new Dialog("PC", "I'm Your Father"));
        GameManager.Instance.newConversation(conver);
        yield break;
    }

    public IEnumerator thrustAttack()
    {
        anim.SetInteger("skill", (int)Ability.thrust);
        yield return new WaitForSeconds(setting.KittyThrust.actDuration / 2);

        GameObject gameo = Instantiate(thrustAttackPrefab);
        KittyThrustAttack thrust = gameo.GetComponent<KittyThrustAttack>();
        thrust.init(this, setting.KittyThrust);
        gameo.transform.parent = transform;
        gameo.transform.localPosition = new Vector2(-3.2f, 0.25f);
        gameo.transform.localScale = new Vector3(4.7f, 3.2f, 1);
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

        GameObject go = Instantiate(enragePrefab);
        go.transform.parent = transform;
        go.transform.localPosition = new Vector3(-0.2f, 0, 0);
        go.transform.localScale = new Vector3(2.67f, 2.67f, 1);
        KittyEnrage a = go.GetComponent<KittyEnrage>();
        a.init(this, setting.KittyEnrage);
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
        setting.KittyThrust.damage *= setting.enrageEnhancement;
        setting.KittyThrust.targetForce *= setting.enrageEnhancement;
        setting.slash.cd = 2f;
        setting.KittyThrust.cd = 3f;
        setting.idleTime = new FloatRange(0.5f, 1.5f);
        setting.idleProAfterLeap = 0.3f;
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
        if (Random.value < setting.idleProAfterLeap)
        {
            setting.idle.actDuration = setting.idleTime.random();
            useSkill("idle", setting.idle, false, true);
        }
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

        GameObject go1 = Instantiate(slash1Prefab);
        go1.transform.parent = transform;
        go1.transform.localPosition = new Vector3(-1.9f, 0, 0);
        go1.transform.localScale = new Vector3(7.15f, 7.15f, 1);
        KittySlash slash1 = go1.GetComponent<KittySlash>();
        slash1.init(this, setting.slash);
        yield return new WaitForSeconds(setting.slash.actDuration * 0.35f);

        GameObject go2 = Instantiate(slash2Prefab);
        go2.transform.parent = transform;
        go2.transform.localPosition = new Vector3(-1.6f, 0, 0);
        go2.transform.localScale = new Vector3(7.15f, 7.15f, 1);
        KittySlash slash2 = go2.GetComponent<KittySlash>();
        slash2.init(this, setting.slash);
        slashing = false;
        yield return new WaitForSeconds(setting.slash.actDuration * 0.4f);

        anim.SetInteger("skill", (int)Ability.idle);
        yield break;
    }

    public IEnumerator shadow()
    {
        int times = 0;
        Vector3 initPosition;

        shadowAttacking = true;
        antiStaggerTime = float.PositiveInfinity;
        anim.SetInteger("skill", (int)Ability.shadowFadeOut);
        body.gravityScale = 0;
        body.AddForce(new Vector2(0, 180));
        yield return new WaitForSeconds(setting.shadowFadeOutTime);

        body.velocity = Vector2.zero;
        initPosition = transform.position;
        yield return new WaitForSeconds(setting.shadowSilenceTime.random());

        // Start attack
        anim.SetInteger("skill", (int)Ability.shadowAttack);
        setting.moveSpeed = setting.shadowAttackSpeed;
        GameObject go = Instantiate(shadowAttackPrefab);
        go.transform.parent = transform;
        go.transform.localPosition = new Vector3(-2.23f, 0.04f, 0);
        go.transform.localScale = new Vector3(4.57f, 4.57f, 1);
        EnemyCommonAttack attack = go.GetComponent<EnemyCommonAttack>();
        attack.init(this, setting.shadowAttack);
        while (times < setting.shadowAttackTimes.random())
        {
            // Random start position
            Vector2 position = ai.targetPlayer.transform.position;
            bool attackFromSky = Random.value > 0.5 ? false : true;
            bool attackFromRight = Random.value > 0.5 ? false : true;
            if (attackFromRight)
                position.x += setting.shadowPosition.x;
            else
                position.x -= setting.shadowPosition.x;
            if (attackFromSky)
                position.y += setting.shadowPosition.y;

            // Start attack
            transform.position = position;
            Vector2 playerPosition = new Vector2(ai.targetPlayer.transform.position.x, ai.targetPlayer.transform.position.y);

            float timer = 0;
            
            // Attack Duration
            while (true)
            {
                move((playerPosition - position).normalized);
                timer += Time.deltaTime;
                if (!attackFromSky && timer > 0.1f)
                    break;
                if (attackFromSky && grounded)
                    break;
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(0.3f);

            timer = 0;
            while (timer < setting.shadowAttackFadeOutTime)
            {
                yield return new WaitForEndOfFrame();
                float t = timer / setting.shadowAttackFadeOutTime;
                GetComponent<SpriteRenderer>().material.color = Color.Lerp(Color.white, Color.clear, t);
                timer += Time.deltaTime;
            }

            transform.position = new Vector3(0, 10000, transform.position.z);
            yield return new WaitForSeconds(setting.shadowSilenceTime.random());
            GetComponent<SpriteRenderer>().material.color = Color.white;
            attack.reActive();
            times += 1;
        }

        anim.SetInteger("skill", (int)Ability.shadowFadeIn);
        Destroy(go);
        yield return new WaitForEndOfFrame();
        transform.position = initPosition;
        ai.faceToPlayer();
        setting.moveSpeed = new KittySet().moveSpeed;
        body.velocity = Vector2.zero;
        body.gravityScale = 0.5f;
        yield return new WaitForSeconds(setting.shadowFadeInTime);

        shadowAttacking = false;
        antiStaggerTime = 0;
        actingTime = 0;
        movementFreezenTime = 0;
        body.gravityScale = 1;
        anim.SetInteger("skill", (int)Ability.idle);

        setting.idle.actDuration = setting.idleTimeAfterShadow.random();
        useSkill("idle", setting.idle, false, true);
        yield break;
    }

    public IEnumerator idle()
    {
        yield break;
    }
}
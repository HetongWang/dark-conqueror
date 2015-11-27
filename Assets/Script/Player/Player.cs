using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Character 
{
    protected bool jumped = false;
    protected bool dashed = false;

    public GameObject normalAttackPrefab;
    public GameObject commonAttackPrefab;
    public GameObject dropAttackPrefab;

    protected Dictionary<string, int> buttonCount = new Dictionary<string, int>();
    protected Dictionary<string, float> buttonCooler = new Dictionary<string, float>();
    protected Dictionary<string, float> buttonCoolerTime = new Dictionary<string, float>();
    protected int normalAttackPhase = 0;

    private bool normalAttacking = false;
    private bool nextNormalAttack = false;

    public int souls = 0;
    [HideInInspector]
    public int normalAttackLevel = 1;

    public override void Awake()
    {
        base.Awake();
        //anim = GetComponent<Animator>();
        anim = GetComponent<Animator>();
        hp = PlayerSet.hp;
        jumpForce = PlayerSet.jumpForce;
        addButtonDetect("left");
        addButtonDetect("right");
        hurtFlashAmount = 0.5f;

        addSkill("normalAttack", normalAttack);
        addSkill("dodge", dodge);
        addSkill("overheadSwing", overheadSwing);
        addSkill("block", block);
        addSkill("dropAttack", dropAttack);
        souls = 1;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        updateButtonCooler();

        if (Input.GetButton("Block"))
            useSkill("block", PlayerSet.block);
        if (Input.GetButtonUp("Block"))
            stopBlock();
        isDodging(Input.GetAxisRaw("Horizontal"));

        if (Input.GetButtonDown("Jump") && !blocked)
            jumped = true;

        dash();
        if (dashed)
            anim.SetBool("dash", true);
        else
            anim.SetBool("dash", false);

        attack();
    }

    void attack()
    {
        if (Input.GetButtonDown("NormalAttack"))
        {
            if (normalAttacking)
                nextNormalAttack = true;
            if (normalAttackPhase == 0 && !nextNormalAttack)
                useSkill("normalAttack", PlayerSet.NormalAttack[0]);
        }

        if (Input.GetButtonDown("HeavyHit"))
        {
            if (grounded)
                useSkill("overheadSwing", PlayerSet.overheadSwing);
            else
            {
                useSkill("dropAttack", PlayerSet.dropAttack, PlayerSet.dropAttack.actDuration);
            }
        }
    }

    void FixedUpdate()
    {
        float horInput = Input.GetAxisRaw("Horizontal");

        // Detect if dash
        if (dashed)
            horInput *= PlayerSet.dashSpeed;
        if (!blocked)
            run(horInput);

        // Jump
        if (jumped)
        {
            jump();
            jumped = false;
        }
    }

    public void jump()
    {
        if (grounded && actingTime <= 0 && freezenTime <= 0)
            body.AddForce(new Vector2(0, jumpForce));
    }

    public IEnumerator normalAttack()
    {
        if (normalAttackLevel > 1 && normalAttackPhase == 2)
            anim.SetInteger("skill", 4);
        else
            anim.SetInteger("skill", normalAttackPhase + 1);
        normalAttacking = true;
        // Wait sword wave forward
        yield return new WaitForSeconds(0.2f);

        Vector3 position = transform.position;
        if (facingRight) { 
            position.x += PlayerSet.NormalAttack[normalAttackPhase].range / 2;
        }
        else
        {
            position.x -= PlayerSet.NormalAttack[normalAttackPhase].range / 2;
        }
        GameObject go =  (GameObject)Instantiate(normalAttackPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        NormalAttack na = go.GetComponent<NormalAttack>();
        na.setPhase(normalAttackPhase);
        na.setAttr(PlayerSet.NormalAttack[normalAttackPhase]);
        na.setLevel(normalAttackLevel);
        na.transform.parent = transform;

        yield return new WaitForSeconds(PlayerSet.NormalAttack[normalAttackPhase].actDuration - 0.2f);
        normalAttacking = false;

        if (!nextNormalAttack)
        {
            anim.SetInteger("skill", 0);
            normalAttackPhase = 0;
        }
        else
        {
            nextNormalAttack = false;
            normalAttackPhase += 1;
            normalAttackPhase %= 3;
            actingTime = 0;
            useSkill("normalAttack", PlayerSet.NormalAttack[normalAttackPhase]);
        }
        yield break;
    }

    void dash()
    {
        string direction = null;

        if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            buttonCount["right"] += 1;
            direction = "right";
        }
        if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            buttonCount["left"] += 1;
            direction = "left";
        }

        if (multiTapDetect(direction, 2) || multiTapDetect(direction, 2))
            dashed = true;

        if (facingRight && Input.GetButtonUp("Horizontal"))
            dashed = false;
        if (!facingRight && Input.GetButtonUp("Horizontal"))
            dashed = false;
    }

    public IEnumerator block()
    {
        blocked = true;
        anim.SetBool("block", true);
        yield break;
    }

    public void stopBlock()
    {
        blocked = false;
        actingTime = 0;
        movementFreezenTime = 0;
        anim.SetBool("block", false);
    }

    public void isDodging(float horInput)
    {
        if (!blocked || horInput == 0)
            return;

        if (horInput > 0)
        {
            if (!facingRight)
                Flip();
        }
        else if (horInput < 0)
        {
            if (facingRight)
                Flip();
        }
        useSkill("dodge", PlayerSet.dodge, true);
    }

    public IEnumerator dodge()
    {
        invincible = true;
        Vector2 force;
        if (grounded)
            force = Vector2.right * PlayerSet.dodgingForce;
        else
            force = Vector2.right * PlayerSet.dodgingSkyForce;

        if (facingRight)
            body.AddForce(force);
        else
            body.AddForce(-force);
        yield return new WaitForSeconds(PlayerSet.dodge.actDuration);

        invincible = false;
    }

    public override void Hurt(SkillSetting setting, Character source)
    {
        base.Hurt(setting, source);
        if (!invincible && !blocked)
        {
            normalAttackPhase = 0;
            normalAttacking = false;
            nextNormalAttack = false;
        }

    }

    bool multiTapDetect(string key, int times)
    {
        if (key == null)
            return false;

        bool res = false;
        if (buttonCount[key] == times && times == 1)
            res = true;

        if (buttonCount[key] == times && buttonCooler[key] > 0)
        {
            res = true;
        }

        buttonCooler[key] = buttonCoolerTime[key];

        return res;
    }

    void updateButtonCooler()
    {
        List<string> keys = new List<string> (buttonCooler.Keys);
        foreach (string s in keys)
        {
            if (buttonCooler[s] > 0)
                buttonCooler[s] -= Time.deltaTime;
            if (buttonCooler[s] <= 0)
                buttonCount[s] = 0;
        }
    }

    void addButtonDetect(string key, float coolertime = 0.5f)
    {
        if (!buttonCooler.ContainsKey(key))
        {
            buttonCooler.Add(key, 0);
            buttonCount.Add(key, 0);
            buttonCoolerTime.Add(key, coolertime);
        }
    }

    public IEnumerator overheadSwing()
    {
        anim.SetInteger("skill", 5);
        yield return new WaitForSeconds(0.5f);

        Vector3 position = childPosition(new Vector2(PlayerSet.overheadSwing.range / 2, 0));
        GameObject go =  (GameObject)Instantiate(commonAttackPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        PCCommonAttack attack = go.GetComponent<PCCommonAttack>();
        attack.init(this, PlayerSet.overheadSwing);
        attack.transform.parent = transform;
        yield return new WaitForSeconds(PlayerSet.overheadSwing.actDuration - 0.5f);

        anim.SetInteger("skill", 0);
        yield break;
    }

    public IEnumerator dropAttack()
    {
        anim.SetInteger("skill", 6);
        body.gravityScale = 2;
        anim.speed = 0;

        while (true)
        {
            if (grounded)
            {
                Vector3 position = childPosition(new Vector2(0, -0.8f));
                GameObject go = (GameObject)Instantiate(dropAttackPrefab, position, Quaternion.Euler(0, 0, 0));
                go.transform.parent = transform;
                DropAttack a = go.GetComponent<DropAttack>();
                a.init(this, PlayerSet.dropAttack);
                StartCoroutine(cancelDropAttack());
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        actingTime = PlayerSet.dropAttack.actDuration;
        movementFreezenTime = actingTime;
        yield return new WaitForSeconds(PlayerSet.dropAttack.actDuration);

        anim.SetInteger("skill", 0);
        yield break;
    }

    public IEnumerator cancelDropAttack()
    {
        anim.speed = 1;
        body.gravityScale = 1;
        yield break;
    }
}

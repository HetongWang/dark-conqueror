using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Character 
{
    protected bool jumped = false;
    protected bool dashed = false;
    protected bool blocked = false;

    public GameObject normalAttackPrefab;

    protected Dictionary<string, int> buttonCount = new Dictionary<string, int>();
    protected Dictionary<string, float> buttonCooler = new Dictionary<string, float>();
    protected Dictionary<string, float> buttonCoolerTime = new Dictionary<string, float>();
    protected int normalAttackPhase = 0;

    public override void Awake()
    {
        base.Awake();
        //anim = GetComponent<Animator>();
        addSkill("normalAttack", normalAttack);
        addSkill("dodge", dodge);
        anim = GetComponent<Animator>();

        hp = PlayerSet.Instance.hp;

        addButtonDetect("left");
        addButtonDetect("right");
        addButtonDetect("normalAttack", 0.8f);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        updateButtonCooler();

        block();
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
            buttonCount["normalAttack"] += 1;
            useSkill("normalAttack", PlayerSet.Instance.NormalAttack);
        }

    }

    void FixedUpdate()
    {
        float horInput = Input.GetAxisRaw("Horizontal");

        // Detect if dash
        if (dashed)
            horInput *= PlayerSet.Instance.dashSpeed;
        if (!blocked)
            run(horInput);

        // Jump
        if (jumped)
        {
            jump();
            jumped = false;
        }
    }

    public IEnumerator normalAttack()
    {
        detectNormalAttackPhase();
        switch (normalAttackPhase)
        {
            case 0:
                anim.SetInteger("attack", 1);
                break;
            case 1:
                anim.SetInteger("attack", 2);
                break;
            case 2:
                anim.SetInteger("attack", 1);
                break;

        }
        yield return new WaitForSeconds(0.2f);

        Vector3 position = transform.position;
        if (facingRight) { 
            position.x += PlayerSet.Instance.NormalAttack.range / 2;
        }
        else
        {
            position.x -= PlayerSet.Instance.NormalAttack.range / 2;
        }
        GameObject go =  (GameObject)Instantiate(normalAttackPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        NormalAttack na = go.GetComponent<NormalAttack>();
        na.transform.parent = transform;
        na.setPhase(normalAttackPhase);

        yield return new WaitForSeconds(PlayerSet.Instance.NormalAttack.actDuration - 0.2f);

        anim.SetInteger("attack", 0);
        yield break;
    }

    protected void detectNormalAttackPhase()
    {
        string name = "normalAttack";

        if (multiTapDetect(name, 1))
            normalAttackPhase = 0;
        else if (multiTapDetect(name, 2))
            normalAttackPhase = 1;
        else if (multiTapDetect(name, 3))
            normalAttackPhase = 2;
        else
            normalAttackPhase = buttonCount[name] % 3;
        Debug.Log(normalAttackPhase);
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

    public void block()
    {
        if (Input.GetButtonDown("Block"))
        {
            blocked = true;
        }
        
        if (Input.GetButtonUp("Block"))
        {
            blocked = false;
        }
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
        useSkill("dodge", PlayerSet.Instance.Dodge, true);
    }

    public IEnumerator dodge()
    {
        invincible = true;
        if (facingRight)
            body.AddForce(Vector2.right * PlayerSet.Instance.dodgingForce);
        else
            body.AddForce(Vector2.left * PlayerSet.Instance.dodgingForce);
        yield return new WaitForSeconds(PlayerSet.Instance.Dodge.actDuration);

        invincible = false;
    }

    public override void Hurt(SkillSetting setting)
    {
        if (!invincible && !blocked)
        {
            getDemage(setting.demage);
            freezenTime = setting.freezenTime;

            if (anim)
            {
                anim.SetBool("hurt", true);
            }
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

}

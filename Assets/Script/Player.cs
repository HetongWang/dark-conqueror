using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Character 
{
    protected bool jumped = false;
    protected bool dashed = false;

    public float dashSpeed = 2f;

    public GameObject normalAttack;
    private Animator anim;

    protected Dictionary<string, int> buttonCount = new Dictionary<string, int>();
    protected Dictionary<string, float> buttonCooler = new Dictionary<string, float>();
    protected Dictionary<string, float> buttonCoolerTime = new Dictionary<string, float>();
    protected int normalAttackPhase = 0;

    public override void Awake()
    {
        base.Awake();
        //anim = GetComponent<Animator>();
        addSkill("normalAttack", doNormalAttack, PlayerSet.Instance.NormalAttack.cd);
        anim = GetComponent<Animator>();

        hp = PlayerSet.Instance.hp;

        addButtonDetect("left");
        addButtonDetect("right");
        addButtonDetect("normalAttack", 0.6f);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        updateButtonCooler();

        if (Input.GetButtonDown("Jump"))
            jumped = true;

        dash();

        if (Input.GetButtonDown("NormalAttack"))
        {
            useSkill("normalAttack", PlayerSet.Instance.NormalAttack);
        }
    }

    void FixedUpdate()
    {
        float horInput = Input.GetAxisRaw("Horizontal");
        // Detect if dash
        if (dashed)
            horInput *= dashSpeed;
        run(horInput);

        // Jump
        if (jumped)
        {
            jump();
            jumped = false;
        }
    }

    public IEnumerator doNormalAttack()
    {
        GameObject go =  (GameObject)Instantiate(normalAttack, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        NormalAttack na = go.GetComponent<NormalAttack>();
        na.transform.parent = transform;

        detectNormalAttackPhase();
        na.setPhase(normalAttackPhase);
        switch (normalAttackPhase)
        {
            case 0:
                anim.SetInteger("attack", 1);
                break;
            case 1:
                anim.SetInteger("attack", 1);
                break;
            case 2:
                anim.SetInteger("attack", 1);
                break;

        }

        yield return new WaitForSeconds(PlayerSet.Instance.NormalAttack.duration);

        anim.SetInteger("attack", 0);
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

    bool multiTapDetect(string key, int times)
    {
        if (key == null)
            return false;

        bool res = false;
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

    protected void detectNormalAttackPhase()
    {
        string name = "normalAttack";
        buttonCount[name] += 1;

        if (multiTapDetect(name, 1))
            normalAttackPhase = 0;
        else if (multiTapDetect(name, 2))
            normalAttackPhase = 1;
        else if (multiTapDetect(name, 3))
            normalAttackPhase = 2;
        else
            normalAttackPhase = buttonCount[name] % 3;
    }
}

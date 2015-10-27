using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Character 
{
    protected bool jumped = false;
    protected bool dashed = false;

    public float dashSpeed = 2f;

    public GameObject normalAttack;
    public GameObject hpSlider;
    public Animator anim;

    protected Dictionary<string, int> buttonCount;
    protected Dictionary<string, float> buttonCooler;
    protected float buttonCoolerTime = 0.5f;
    protected int normalAttackPhase = 0;
    protected float normalAttackCooler;
    public float normalAttackTime = 1.5f;

    public override void Awake()
    {
        base.Awake();
        //anim = GetComponent<Animator>();
        buttonCooler = new Dictionary<string, float>();
        buttonCount = new Dictionary<string, int>();
        addSkill("normalAttack", doNormalAttack, SkillSetting.Instance.NormalAttack.cd);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Jump"))
            jumped = true;
        Dash();
        updateNormalAttackCounter();
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

        if (Input.GetButtonDown("Fire1"))
        {
            useSkill("normalAttack", SkillSetting.Instance.NormalAttack);
        }
    }

    public IEnumerator doNormalAttack()
    {
        GameObject go =  (GameObject)Instantiate(normalAttack, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        NormalAttack na = go.GetComponent<NormalAttack>();
        na.setPhase(normalAttackPhase);
        normalAttackCooler = normalAttackTime;
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

        normalAttackPhase += 1;
        yield return new WaitForSeconds(SkillSetting.Instance.NormalAttack.duration);

        anim.SetInteger("attack", 0);
        yield break;
    }

    void Dash()
    {
        if (DoubleTapDetect("left") || DoubleTapDetect("right"))
            dashed = true;

        if (facingRight && Input.GetButtonUp("Horizontal"))
            dashed = false;
        if (!facingRight && Input.GetButtonUp("Horizontal"))
            dashed = false;
    }

    bool DoubleTapDetect(string key)
    {
        bool isKeyPress = false;
        bool res = false;

        if (key == "right")
        { 
            if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
                isKeyPress = true;
        }
        else if (key == "left")
        {
            if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
                isKeyPress = true;
        }  
        else if (Input.GetButtonDown(key))
        {
            isKeyPress = true;
        }

        if (!buttonCooler.ContainsKey(key))
        {
            buttonCooler.Add(key, 0);
            buttonCount.Add(key, 0);
        }

        List<string> keys = new List<string> (buttonCooler.Keys);
        foreach (string s in keys)
        {
            if (buttonCooler[s] > 0)
                buttonCooler[s] -= Time.deltaTime;
            if (buttonCooler[s] <= 0)
                buttonCount[s] = 0;
        }

        if (isKeyPress)
        {
            buttonCount[key] += 1;
            if (buttonCount[key] == 1)
            {
                buttonCooler[key] = buttonCoolerTime;
            }

            if (buttonCount[key] == 2 && buttonCooler[key] > 0)
            {
                buttonCount[key] = 0;
                res = true;
            }
        }

        return res;
    }

    protected void updateNormalAttackCounter()
    {
        normalAttackCooler -= Time.deltaTime;
        if (normalAttackCooler <= 0)
            normalAttackPhase = 0;
    }
}

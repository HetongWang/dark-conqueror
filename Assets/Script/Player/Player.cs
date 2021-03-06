﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Character 
{
    [HideInInspector]
    public PlayerSet setting;
    public float stamina;
    public float magic;
    protected bool jumped = false;

    public GameObject normalAttackPrefab;
    public GameObject commonAttackPrefab;
    public GameObject overheadSwingPrefab;
    public GameObject dropAttackPrefab;
    public GameObject eruptionFirePrefab;

	public AudioClip swordSwing1;
	public AudioClip swordSwing2;
	public AudioClip swordSwing3;
	public AudioClip swordDrop;
	public AudioClip shieldSound;
	public AudioClip hitSound;

	public AudioSource playerAudio;

    protected Dictionary<string, int> buttonCount = new Dictionary<string, int>();
    protected Dictionary<string, float> buttonCooler = new Dictionary<string, float>();
    protected Dictionary<string, float> buttonCoolerTime = new Dictionary<string, float>();
    protected int normalAttackPhase = 0;

    private bool normalAttacking = false;
    private bool nextNormalAttack = false;

    [HideInInspector]
    public int normalAttackLevel = 1;

    public override void Awake()
    {
        base.Awake();
        _setting = new PlayerSet();
        setting = (PlayerSet)_setting;
        hp = setting.hp;
        stamina = setting.stamina;
        magic = setting.magic;

        //anim = GetComponent<Animator>();
        anim = GetComponent<Animator>();
        addButtonDetect("left");
        addButtonDetect("right");
        hurtFlashAmount = 0.5f;

		playerAudio = GetComponent<AudioSource>();
		
        addSkill("normalAttack", normalAttack);
        addSkill("dodge", dodge);
        addSkill("overheadSwing", overheadSwing);
        addSkill("block", block);
        addSkill("dropAttack", dropAttack);
        addSkill("eruptionFire", eruptionFire);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        updateButtonCooler();

        if (Input.GetButton("Block") && stamina > 1f) 
            useSkill("block", setting.block);
        if (Input.GetButtonUp("Block") || stamina <= 0)
            stopBlock();
        isDodging(Input.GetAxisRaw("Horizontal"));

        if (Input.GetButtonDown("Jump") && !blocked)
            jumped = true;

        attack();
    }

    public override void updateStatus()
    {
        base.updateStatus();
        anim.SetFloat("verticalSpeed", body.velocity.y);

        // Update Stamina
        bool staminaCost = false;
        if (blocked)
        { 
            stamina -= setting.blockCost * Time.deltaTime;
            staminaCost = true;
        }
        if (!staminaCost)
            stamina += setting.staminaRecoverSpeed * Time.deltaTime;
        if (stamina > setting.stamina)
            stamina = setting.stamina;
        if (stamina < 0)
            stamina = 0;

        // Update Magic
        magic += setting.magicRecoverSpeed * Time.deltaTime;
        if (magic > setting.magic)
            magic = setting.magic;
        if (magic < 0)
            magic = 0;

    }

    void attack()
    {
        if (Input.GetButtonDown("NormalAttack"))
        {
			playerAudio.pitch = .62F;
			playerAudio.clip = swordSwing1;
            if (normalAttacking){

                nextNormalAttack = true;
			}
            if (normalAttackPhase == 0 && !nextNormalAttack){
                useSkill("normalAttack", setting.normalAttack[0]);
			}
        }

        if (Input.GetButtonDown("HeavyHit"))
        {

            if (grounded){
				playerAudio.clip = swordSwing1;
				playerAudio.pitch = .4F;
                useSkill("overheadSwing", setting.overheadSwing);
			}
            else
            {
				playerAudio.clip = swordDrop;
				playerAudio.pitch = 1F;
                useSkill("dropAttack", setting.dropAttack, setting.dropAttack.actDuration);
            }
        }

        if (Input.GetButtonDown("Magic") && magic >= setting.eruptionFireCost)
        {
            useSkill("eruptionFire", setting.eruptionFire);
        }
    }

    void FixedUpdate()
    {
        float horInput = Input.GetAxisRaw("Horizontal");

        // Detect if dash
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
            body.AddForce(new Vector2(0, setting.jumpForce));
    }

    public override IEnumerator dying()
    {
        Time.timeScale = 0;
        GameManager.Instance.activeDeadUI();
        yield break;
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
            position.x += setting.normalAttack[normalAttackPhase].range / 2;
        }
        else
        {
            position.x -= setting.normalAttack[normalAttackPhase].range / 2;
        }
        GameObject go =  (GameObject)Instantiate(normalAttackPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        NormalAttack na = go.GetComponent<NormalAttack>();
        na.init(this, setting.normalAttack[normalAttackPhase]);
        na.setPhase(normalAttackPhase);
        na.setLevel(normalAttackLevel);
        na.transform.parent = transform;

        yield return new WaitForSeconds(setting.normalAttack[normalAttackPhase].actDuration - 0.2f);
        normalAttacking = false;

        if (Input.GetAxisRaw("Horizontal") > 0 && !facingRight)
            Flip();
        if (Input.GetAxisRaw("Horizontal") < 0 && facingRight)
            Flip();
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
            useSkill("normalAttack", setting.normalAttack[normalAttackPhase]);
        }
        playerAudio.clip = null;
        yield break;
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
        if (horInput == 0 || !grounded)
            return;
        if (stamina < setting.dashCost)
            return;

        string direction = null;

        if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            buttonCount["right"] += 1;
            buttonCount["right"] %= 3;
            direction = "right";
        }
        if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            buttonCount["left"] += 1;
            buttonCount["left"] %= 3;
            direction = "left";
        }

        if (multiTapDetect(direction, 2) || multiTapDetect(direction, 2))
            if (freezenTime <= 0)
                useSkill("dodge", setting.dodge, true, true);
    }

    public IEnumerator dodge()
    {
        invincible = true;
        anim.SetInteger("skill", 8);
        movementFreezenTime = 0;
        playerAudio.clip = null;
        Vector2 force;
        if (grounded)
            force = Vector2.right * setting.dodgingForce;
        else
            force = Vector2.right * setting.dodgingSkyForce;

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (!facingRight)
                Flip();
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (facingRight)
                Flip();
        }

        if (facingRight)
            body.AddForce(force);
        else
            body.AddForce(-force);
        stamina -= setting.dodgeCost;

        yield return new WaitForSeconds(setting.dodge.actDuration);
        while (body.velocity.x > 3f)
            yield return new WaitForEndOfFrame();

        anim.SetInteger("skill", 0);
        invincible = false;
        normalAttackPhase = 0;
        normalAttacking = false;
        nextNormalAttack = false;
        yield break;
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
        yield return new WaitForSeconds(setting.overheadSwing.actDuration * 0.6f);

        Vector3 position = childPosition(new Vector2(setting.overheadSwing.range / 2, 0));
        GameObject go =  (GameObject)Instantiate(overheadSwingPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        PCCommonAttack attack = go.GetComponent<PCCommonAttack>();
        attack.init(this, setting.overheadSwing);
        attack.transform.parent = transform;
        yield return new WaitForSeconds(setting.overheadSwing.actDuration * 0.4f);

        anim.SetInteger("skill", 0);
        playerAudio.clip = null;
        yield break;
    }

    public IEnumerator dropAttack()
    {
        anim.SetInteger("skill", 6);
        body.gravityScale = 2;
        body.velocity = new Vector2(body.velocity.x / 2, 0);
        anim.speed = 0;

        Vector3 position = childPosition(new Vector2(0, -0.8f));
        GameObject go = (GameObject)Instantiate(dropAttackPrefab, position, Quaternion.Euler(0, 0, 0));
        go.transform.parent = transform;
        go.GetComponent<PointEffector2D>().enabled = false;
        DropAttack a = go.GetComponent<DropAttack>();
        a.init(this, setting.dropAttack);

        while (true)
        {
            if (grounded)
            {
                go.GetComponent<PointEffector2D>().enabled = true;
                StartCoroutine(cancelDropAttack());
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        actingTime = setting.dropAttack.actDuration;
        movementFreezenTime = actingTime;
        yield return new WaitForSeconds(0.1f);
        Destroy(go);
        yield return new WaitForSeconds(setting.dropAttack.actDuration - 0.1f);

        anim.SetInteger("skill", 0);
        playerAudio.clip = null;
        yield break;
    }

    public IEnumerator cancelDropAttack()
    {
        anim.speed = 1;
        body.gravityScale = 1;
        playerAudio.clip = null;
        yield break;
    }

    public IEnumerator eruptionFire()
    {
        anim.SetInteger("skill", 7);
        magic -= setting.eruptionFireCost;
        for (int i = 0; i < setting.eruptionFireTimes; i++)
        {
            float distance = 3.3f * i + 3.6f;
            GameObject go = Instantiate(eruptionFirePrefab);
            go.transform.parent = transform;
            go.transform.localPosition = new Vector2(distance, -2.47f);
            go.transform.localScale = new Vector3(1.336f, 1.336f, 1);
            go.transform.parent = null;
            EruptionFire fire = go.GetComponent<EruptionFire>();
            fire.init(this, setting.eruptionFire);
            yield return new WaitForSeconds(setting.eruptionFire.actDuration / setting.eruptionFireTimes);
        }
        anim.SetInteger("skill", 0);
        yield break;
    }
}

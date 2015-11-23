﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;
    public bool invincible = false;
    protected bool blocked = false;
    public float moveSpeed = 3f;
    public float jumpForce = 600;
    public float hp = 10;
    protected float dyingDuration = 1.1f;
    [HideInInspector]
    public float disappearTime = 4f;

    protected Transform groundCheck;
    protected bool grounded = false;
    protected float movementFreezenTime = 0;
    protected float actingTime = 0;
    protected float freezenTime = 0;

    [HideInInspector]
    public Rigidbody2D body;
    protected Animator anim;
    public StatusEffectController statusController;

    public delegate IEnumerator skillFunction();
    protected Dictionary<string, skillFunction> skills = new Dictionary<string, skillFunction>();
    public Dictionary<string, float> skillCooler = new Dictionary<string, float>();
    protected Coroutine currentSkill = null;

    /// <summary>
    /// Initialize method setting HP, AI, adding skills and other component
    /// </summary>
    public virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        groundCheck = transform.Find("groundCheck");
        statusController = new StatusEffectController(this);
    }

    /// <summary>
    /// Automatically check hp and skill cd
    /// </summary>
    public virtual void Update()
    {
        detectGround();
        AliveOrDie();
        updateStatus();
        updateSkillCD();
        statusController.updateStatus();
    }

    public void updateStatus()
    {
        movementFreezenTime = Mathf.Max(movementFreezenTime, freezenTime);
        if (movementFreezenTime > 0)
        {
            movementFreezenTime -= Time.deltaTime;
        }

        if (freezenTime > 0)
        {
            freezenTime -= Time.deltaTime;
            if (anim)
                anim.SetBool("hurt", true);
        }
        else
        {
            if (anim)
                anim.SetBool("hurt", false);
        }

        if (actingTime > 0)
        {
            actingTime -= Time.deltaTime;
        }

        if (anim)
            anim.SetFloat("moveSpeed", Mathf.Abs(body.velocity.x));
    }

    private void updateSkillCD()
    {
        List<string> keys = new List<string>(skillCooler.Keys);
        foreach (string s in keys)
        {
            if (skillCooler[s] > 0)
                skillCooler[s] -= Time.deltaTime;
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x = scale.x * -1;
        transform.localScale = scale;
    }

    protected bool canActing()
    {
        if (actingTime <= 0 && freezenTime <= 0)
            return true;
        else
            return false;
    }

    public virtual void Hurt(SkillSetting setting, Character source)
    {
        if (!invincible && !blocked)
        {
            getDemage(setting.damage);
            freezenTime = Mathf.Max(setting.freezenTime, freezenTime);
            if (currentSkill != null)
                StopCoroutine(currentSkill);

            if (anim)
            {
                anim.SetInteger("skill", 0);
                anim.SetBool("hurt", true);
            }
        }

        if (setting.targetForce != null)
        {
            Vector2 f = setting.targetForce;
            if (source != null)
            {
                if (source.facingRight)
                    f.x = Mathf.Abs(f.x);
                else
                    f.x = -Mathf.Abs(f.x);
            }
            body.AddForce(f);
        }
    }

    public void getDemage(float amount)
    {
        if (!invincible)
        {
            hp -= amount;
        }
    }

    public void AliveOrDie()
    {
        if (hp <= 0)
        {
            actingTime = 9999999f;
            movementFreezenTime = 99999999f;
            StartCoroutine(dying());
        }

    }

    public virtual IEnumerator dying()
    {
        yield break;
    }

    public void run(float horInput)
    {
        if (movementFreezenTime <= 0)
        {
            move(horInput);
        }
    }

    protected void move(float horInput)
    {
        // Control horizontal speed
        if (horInput != 0)
        {
            Vector2 v = body.velocity;
            if (Mathf.Abs(v.x) < moveSpeed * Mathf.Abs(horInput))
                v.x = moveSpeed * horInput;
            else if (Mathf.Sign(horInput) != Mathf.Sign(v.x))
                v.x += moveSpeed * horInput;
            body.velocity = v;
            //body.velocity = new Vector2(horInput * moveSpeed, body.velocity.y);
        }

        // Change facing direct
        if ((horInput > 0 && !facingRight) || (horInput < 0 && facingRight))
        {
            Flip();
        }
    }

    protected void move(float horInput, float verInput)
    {
        move(horInput);
        if (verInput != 0)
            body.velocity = new Vector2(body.velocity.x, verInput * moveSpeed);
        else
        {
            body.velocity = new Vector2(body.velocity.x, 0);
        }
    }

    protected void detectGround()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (anim)
            anim.SetBool("grounded", grounded);
    }

    /// <summary>
    /// Add skills. Use useSkill function to release skill
    /// </summary>
    /// <param name="name">skill name</param>
    /// <param name="f">skill IEnumerator function</param>
    /// <param name="cd"></param>
    public void addSkill(string name, skillFunction f, float initCD = 0)
    {
        skills.Add(name, f);
        skillCooler.Add(name, initCD);
    }

    public void useSkill(string name, SkillSetting setting, bool canMove = false, bool enforce = false)
    {
        if (name != null && skillCooler[name] <= 0 && (canActing() || enforce))
        {
            Debug.Log(name);
            if (enforce && currentSkill != null)
                StopCoroutine(currentSkill);

            actingTime = setting.actDuration + 0.05f;
            currentSkill = StartCoroutine(skills[name]());
            skillCooler[name] = setting.cd;

            if (!canMove)
            {
                movementFreezenTime = setting.actDuration;
            }
        }
    }

    public Vector3 childPosition(Vector2 offset)
    {
        Vector3 position = transform.position;
        if (facingRight)
        {
            position.x += Mathf.Abs(offset.x);
        }
        else
        {
            position.x -= Mathf.Abs(offset.x);
        }
        position.y += offset.y;
        return position;
    }

    public void addSelfForce(Vector2 force)
    {
        if (facingRight)
        {
            body.AddForce(force);
        }
        else
        {
            Vector2 f = new Vector2(-force.x, force.y);
            body.AddForce(f);
        }
    }
}

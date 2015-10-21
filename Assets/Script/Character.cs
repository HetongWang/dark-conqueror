﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {
    [HideInInspector]
    public bool facingRight = true;
    public float moveSpeed = 3f;

    public float jumpForce = 600;
    protected Transform groundCheck;
    protected bool grounded = false;

    public float hp = 10;
    protected Rigidbody2D body;

    public delegate IEnumerator skillFunction();
    protected bool acting = false;
    protected Dictionary<string, skillFunction> skills = new Dictionary<string, skillFunction>();
    public Dictionary<string, float> skillCooler = new Dictionary<string, float>();

    public virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        groundCheck = transform.Find("groundCheck");
    }

    public virtual void Update()
    {
        // Check if grounded
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        AliveOrDie();
        updateSkillCD();
    }

    private void updateSkillCD()
    {
        List<string> keys = new List<string> (skillCooler.Keys);
        foreach (string s in keys)
        {
            if (skillCooler[s] > 0)
                skillCooler[s] -= Time.deltaTime;
        }
    }



    protected void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x = scale.x * -1;
        transform.localScale = scale;
    }

    public void Hurt(float amount = 0)
    {
        hp -= amount;
    }

    public void AliveOrDie()
    {
        if (hp <= 0)
            Destroy(gameObject);
    }

    public void run(float horInput)
    {
        if (!acting)
        {
            move(horInput);
        }
    }

    public void move(float horInput)
    {
        // Control horizontal speed
        if (horInput != 0)
            body.velocity = new Vector2(horInput * moveSpeed, body.velocity.y);
        else
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }

        // Change facing direct
        if ((horInput > 0 && !facingRight) || (horInput < 0 && facingRight))
        {
            Flip();
        }
    }

    public void move(float horInput, float verInput)
    {
        move(horInput);
        if (verInput != 0)
            body.velocity = new Vector2(body.velocity.x, verInput * moveSpeed);
        else
        {
            body.velocity = new Vector2(body.velocity.x, 0);
        }
    }

    public void jump()
    {
        if (grounded)
            body.AddForce(new Vector2(0, jumpForce));
    }

    protected void addSkill(string name, skillFunction f, float cd)
    {
        skills.Add(name, f);
        skillCooler.Add(name, cd);
    }

    public void useSkill(string name, float cd)
    {
        if (name != null && skillCooler[name] <= 0)
        {
            acting = true;
            StartCoroutine(skills[name]());
            skillCooler[name] = cd; 
        }
    }
}

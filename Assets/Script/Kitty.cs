﻿using UnityEngine;
using System.Collections;

public class Kitty: Character
{
    private KittyAI ai;

    public GameObject thrustAttackScript;
    protected int animAttack;
    protected Animator anim;

    public override void Awake()
    {
        base.Awake();
        addSkills();
        facingRight = false;
        hp = 30;
        ai = new KittyAI(this);
        anim = GetComponent<Animator>();
    }

    void addSkills()
    {
        string name = "thrust";
        skillCooler.Add(name, 2);
        skillRange.Add(name, 3);
        skillDuration.Add(name, 0.083f);
    }

    public override void Update()
    {
        base.Update();
        string attack = ai.attack();
        switch (attack)
        {
            case "thrust":
                thrustAttack();
                break;
        }
    }

    void FixedUpdate()
    {
        float horMove = ai.horMove(transform.position);
        run(horMove);
    }

    public IEnumerator thrustAttack()
    {
        Vector3 position = transform.position;
        anim.SetInteger("attack", 1);

        if (facingRight)
            position.x += skillRange["thrust"];
        else
            position.x -= skillRange["thrust"];
        Instantiate(thrustAttackScript, position, Quaternion.Euler(new Vector3(0, 0, 0)));

        yield return new WaitForSeconds(KittyThrustAttack.duration);
        anim.SetInteger("attack", 0);
    }
}
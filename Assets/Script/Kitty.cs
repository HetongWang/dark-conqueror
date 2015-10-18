using UnityEngine;
using System.Collections;

public class Kitty: Character
{
    private KittyAI ai;

    public GameObject thrustAttackScript;

    public override void Awake()
    {
        base.Awake();
        addSkills();
        facingRight = false;
        hp = 30;
        ai = new KittyAI(this);
    }

    void addSkills()
    {
        string name = "thrust";
        skillCooler.Add(name, 2);
        skillRange.Add(name, 3);
    }

    public override void Update()
    {
        base.Update();
        string attack = ai.attack();
        Debug.Log(attack);
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
        move(horMove);
    }

    public void thrustAttack()
    {
        Vector3 position = transform.position;
        if (facingRight)
            position.x += skillRange["thrust"];
        else
            position.x -= skillRange["thrust"];
        Instantiate(thrustAttackScript, position, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
}
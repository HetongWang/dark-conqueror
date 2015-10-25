using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SiegeBow : Character 
{
    protected Vector2 javelinVelocity;
    protected SiegeBowAI ai;

    public override void Awake()
    {
        base.Awake();
        movement = false;
        hp = 20;
        ai = new SiegeBowAI(this);
        addSkill("shoot", shoot, SkillSetting.Instance.SiegeBowShoot.cd);
    }

    public override void Update()
    {
        base.Update();
        if (skillCooler["shoot"] <= 0)
        {
            javelinVelocity = ai.shootVelocity();
            useSkill("shoot", SkillSetting.Instance.SiegeBowShoot.cd);
        }
    }

    // To do
    public IEnumerator shoot()
    {
        yield break;
    }
}

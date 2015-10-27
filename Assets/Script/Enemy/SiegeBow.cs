using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SiegeBow : Character 
{
    protected Vector2 javelinVelocity;
    protected SiegeBowAI ai;

    public GameObject javelinPrefab;

    public override void Awake()
    {
        base.Awake();
        movement = false;
        hp = 20;
        ai = new SiegeBowAI(this);
        ai.seekPlayer();
        addSkill("shoot", shoot, SkillSetting.Instance.SiegeBowShoot.cd);
    }

    public override void Update()
    {
        base.Update();
        if (skillCooler["shoot"] <= 0)
        {
            javelinVelocity = ai.shootVelocity();
            useSkill("shoot", SkillSetting.Instance.SiegeBowShoot);
        }
    }

    // To do
    public IEnumerator shoot()
    {
        GameObject go =  (GameObject)Instantiate(javelinPrefab, transform.position, Quaternion.Euler(0, 0, ai.angle));
        Javelin jl = go.GetComponent<Javelin>();
        jl.setInitVelocity(javelinVelocity);
        yield break;
    }
}

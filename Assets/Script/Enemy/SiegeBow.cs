using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SiegeBow : Character 
{
    protected Vector2 javelinVelocity;
    protected SiegeBowAI ai;

    public float minShootRange = 1f;
    public GameObject javelinPrefab;

    public override void Awake()
    {
        base.Awake();
        movement = false;
        hp = 20;
        ai = new SiegeBowAI(this);
        ai.seekPlayer();
        addSkill("shoot", shoot, SiegeBowSet.Instance.SiegeBowShoot.cd);
    }

    public override void Update()
    {
        base.Update();
        if (couldShoot())
        {
            javelinVelocity = ai.shootVelocity();
            useSkill("shoot", SiegeBowSet.Instance.SiegeBowShoot);
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

    bool couldShoot()
    {
        bool res = true;
        if (skillCooler["shoot"] > 0)
            res = false;
        if (ai.targetPlayer.transform.position.x - transform.position.x > -minShootRange)
            res = false;

        return res;
    }
}

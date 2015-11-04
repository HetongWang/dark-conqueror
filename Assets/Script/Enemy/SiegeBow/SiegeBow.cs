using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SiegeBow : Enemy
{
    protected Vector2 javelinVelocity;
    protected SiegeBowAI ai;

    public float minShootRange = 1f;
    public GameObject javelinPrefab;

    public override void Awake()
    {
        base.Awake();
        addSkill("shoot", shoot, SiegeBowSet.Instance.SiegeBowShoot.cd);
        movement = false;
        hp = SiegeBowSet.Instance.hp;

        ai = new SiegeBowAI(this);
        ai.seekPlayer();
        setHPBar(SiegeBowSet.Instance.hpBarOffset, SiegeBowSet.Instance.hp);
    }

    public override void Update()
    {
        base.Update();
        if (couldShoot())
        {
            javelinVelocity = ai.shootVelocity();
            if (!float.IsNaN(javelinVelocity.x))
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

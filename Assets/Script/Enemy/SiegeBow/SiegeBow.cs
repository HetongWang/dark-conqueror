using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SiegeBow : Enemy
{
    public GameObject javelinPrefab;

    public float angle = Mathf.Deg2Rad * 30;

    public override void Awake()
    {
        base.Awake();
        addSkill("shoot", shoot, SiegeBowSet.SiegeBowShoot.cd);
        hp = SiegeBowSet.hp;
        dyingDuration = 0f;
        disappearTime = 0f;

        ai = new SiegeBowAI(this);
        setHPBar(SiegeBowSet.hpBarOffset, SiegeBowSet.hp);
    }

    public override void Update()
    {
        base.Update();
        switch (behavior)
        {
            case "shoot":
                useSkill("shoot", SiegeBowSet.SiegeBowShoot);
                break;
        }
    }

    public IEnumerator shoot()
    {
        GameObject go =  (GameObject)Instantiate(javelinPrefab, transform.position, Quaternion.Euler(0, 0, angle));
        Javelin jl = go.GetComponent<Javelin>();
        jl.owner = this;
        SiegeBowAI bowAI = (SiegeBowAI)ai;
        jl.setInitVelocity(bowAI.velocity);
        yield break;
    }
}

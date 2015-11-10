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
        addSkill("shoot", shoot, SiegeBowSet.Instance.SiegeBowShoot.cd);
        hp = SiegeBowSet.Instance.hp;

        ai = new SiegeBowAI(this);
        setHPBar(SiegeBowSet.Instance.hpBarOffset, SiegeBowSet.Instance.hp);
    }

    public override void Update()
    {
        base.Update();
        switch (behavior)
        {
            case "shoot":
                useSkill("shoot", SiegeBowSet.Instance.SiegeBowShoot);
                break;
        }
    }

    public IEnumerator shoot()
    {
        GameObject go =  (GameObject)Instantiate(javelinPrefab, transform.position, Quaternion.Euler(0, 0, angle));
        Javelin jl = go.GetComponent<Javelin>();
        SiegeBowAI bowAI = (SiegeBowAI)ai;
        jl.setInitVelocity(bowAI.velocity);
        yield break;
    }
}

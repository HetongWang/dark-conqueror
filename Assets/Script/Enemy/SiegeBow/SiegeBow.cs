﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SiegeBow : Enemy
{
    public GameObject javelinPrefab;

    public float angle = Mathf.Deg2Rad * 30;
    public SiegeBowSet setting;

    public override void Awake()
    {
        base.Awake();
        _setting = new SiegeBowSet();
        setting = (SiegeBowSet)_setting;

        dyingDuration = 0f;
        disappearTime = 0f;
        ai = new SiegeBowAI(this);
        setHPBar(setting.hpBarOffset, setting.hp);
        addSkill("shoot", shoot, setting.SiegeBowShoot.cd);
    }

    public override void Update()
    {
        base.Update();
        switch (behavior)
        {
            case "shoot":
                useSkill("shoot", setting.SiegeBowShoot);
                break;
        }
    }

    public IEnumerator shoot()
    {
        GameObject go =  (GameObject)Instantiate(javelinPrefab, transform.position, Quaternion.Euler(0, 0, angle));
        Javelin jl = go.GetComponent<Javelin>();
        jl.init(this, setting.SiegeBowShoot);
        SiegeBowAI bowAI = (SiegeBowAI)ai;
        jl.setInitVelocity(bowAI.velocity);
        yield break;
    }
}

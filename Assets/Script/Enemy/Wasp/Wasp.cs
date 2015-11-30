using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wasp : Enemy
{
    public GameObject attackPrefab;
    [HideInInspector]
    public WaspSet setting;

    public override void Awake()
    {
        base.Awake();
        _setting = new WaspSet();
        setting = (WaspSet)_setting;
        behavior = "high";

        ai = new SiegeBowAI(this);
        setHPBar(setting.hpBarOffset, setting.hp);
    }

    public override void Update()
    {
        base.Update();
        switch (behavior)
        {

        }
    }

}

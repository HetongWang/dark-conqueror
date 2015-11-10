using UnityEngine;
using System.Collections.Generic;

public class SkillSetting {
    public float 
        cd = 0, 
        actDuration = 0.5f,     // animation time or attackspeed
        demage = 0, 
        range = 0, 
        attackDuration = 0.1f,  // attack instance survive time. 
        freezenTime = 0f;       // target stagger time

    public SkillSetting()
    {

    }

    /// <summary>
    /// set basic skill attr
    /// </summary>
    /// <param name="actDuration">animation time</param>
    /// <param name="attackDuration">attack survive time. 0 means not destory auto. default 0.1</param>
    public SkillSetting(float actDuration, float cd, float demage, float range, float attackDuration = 0.1f)
    {
        this.actDuration = actDuration;
        this.cd = cd;
        this.demage = demage;
        this.range = range;
        this.attackDuration = attackDuration;
    }
}

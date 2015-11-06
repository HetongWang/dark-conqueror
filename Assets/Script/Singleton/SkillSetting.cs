using UnityEngine;
using System.Collections.Generic;

public class SkillSetting {
    public float 
        cd, 
        actDuration, 
        demage, 
        range, 
        attackDuration = 0.1f, 
        freezenTime = 1f;

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

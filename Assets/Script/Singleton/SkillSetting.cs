using UnityEngine;
using System.Collections.Generic;

public class SkillSetting {
    public float 
        cd = 0, 
        actDuration = 0.5f,     // animation time or attackspeed
        damage = 0, 
        range = 0, 
        attackDuration = 0.8f,  // attack instance survive time. 
        freezenTime = 0f;       // target stagger time

    public Vector2 force = Vector2.zero;

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
        this.damage = demage;
        this.range = range;
        this.attackDuration = attackDuration;
    }

    public SkillSetting clone()
    {
        SkillSetting n = new SkillSetting();
        n.cd = cd;
        n.actDuration = actDuration;
        n.freezenTime = freezenTime;
        n.attackDuration = attackDuration;
        n.range = range;
        n.damage = damage;
        return n;
    }

    public static float frameToSeconds(float length, float fps)
    {
        return length / fps;
    }
}

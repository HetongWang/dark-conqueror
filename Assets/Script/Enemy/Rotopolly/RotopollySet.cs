﻿using UnityEngine;

public class RotopollySet : Singleton<RotopollySet>
{
    public Vector2 hpBarOffset = new Vector2(0, 4.5f);

    public float hp = 3;
    public float moveSpeed = 2f;
    public SkillSetting run = new SkillSetting();
    public float runSpeed = 5f;
    public float runAcceleration = 0.5f;
    public SkillSetting jump = new SkillSetting();

    protected RotopollySet()
    {
        run.actDuration = float.PositiveInfinity;
        run.cd = 0f;
        run.attackDuration = run.actDuration;
        run.range = 6f;
        run.damage = 1f;
        run.freezenTime = 0.2f;
        run.targetForce = new Vector2(100, 0);
        run.selfForce = new Vector2(200, 0);

        jump.actDuration = 0f;
        jump.selfForce = new Vector2(50, 300);
    }
}
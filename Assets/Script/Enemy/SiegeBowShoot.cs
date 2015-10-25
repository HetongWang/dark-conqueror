using UnityEngine;
using System.Collections;

public class SiegeBowShoot : BasicAttack
{
    protected Vector2 initVelocity;

    public override void Awake()
    {
        base.Awake();
        setAttr(SkillSetting.Instance.SiegeBowShoot);
        targetTag = "Player";
    }

    void setInitVelocity(Vector2 v)
    {
        initVelocity = v;
    }
}
using UnityEngine;

public class WaspSet : CharacterSet
{
    public Vector2 hpBarOffset = new Vector2(0, 4.5f);

    public SkillSetting attack = new SkillSetting();
    public float attackMoveSpeed;
    public SkillSetting high = new SkillSetting();
    public SkillSetting low = new SkillSetting();
    public RangeAttribute highFartingTime;
    public RangeAttribute lowFartingTime;
    public float fartingRadius;
    public float refLowHeight;
    public float highHeight;

    public WaspSet()
    {
        hp = 3;
        moveSpeed = 2f;
        souls = 4;
        highFartingTime = new RangeAttribute(1, 3);
        lowFartingTime = new RangeAttribute(2, 4);
        refLowHeight = 2f;
        fartingRadius = 1f;

        attack.actDuration = 1f;
        attack.cd = 2f;
        attack.damage = 1.5f;
        attack.range = 10f;
        attack.attackDuration = 0;
        attackMoveSpeed = 12f;

        high.actDuration = 0;
        high.cd = 1f;
        low.actDuration = 0;
        low.cd = 1f;
    }
}

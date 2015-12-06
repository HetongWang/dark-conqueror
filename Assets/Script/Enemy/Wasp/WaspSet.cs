using UnityEngine;

public class WaspSet : CharacterSet
{
    public Vector2 hpBarOffset = new Vector2(0, 4.5f);

    public SkillSetting attack = new SkillSetting();
    public float attackMoveSpeed;
    public SkillSetting high = new SkillSetting();
    public SkillSetting low = new SkillSetting();
    public FloatRange highFartingTime;
    public FloatRange lowFartingTime;
    public float fartingRadius;
    public float refLowHeight;
    public float highHeight;

    public WaspSet()
    {
        hpBarOffset = new Vector2(0, 1.5f);
        hp = 3;
        moveSpeed = 5f;
        souls = 4;
        highFartingTime = new FloatRange(1, 3);
        lowFartingTime = new FloatRange(2, 4);
        refLowHeight = 1.5f;
        fartingRadius = 1f;

        attack.actDuration = float.PositiveInfinity;
        attack.cd = 6f;
        attack.damage = 1.5f;
        attack.range = 3f;
        attack.attackDuration = 0;
        attackMoveSpeed = 13f;

        high.actDuration = 0.2f;
        high.cd = 1f;
        low.actDuration = 0.2f;
        low.cd = 1f;
    }
}

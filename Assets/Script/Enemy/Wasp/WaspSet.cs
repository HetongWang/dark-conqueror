using UnityEngine;

public class WaspSet : CharacterSet
{
    public Vector2 hpBarOffset = new Vector2(0, 4.5f);

    public SkillSetting attack = new SkillSetting();
    public RangeAttribute highFartingTime;
    public float lowHeight;
    public RangeAttribute lowFartingTime;

    public WaspSet()
    {
        hp = 3;
        moveSpeed = 6f;
        souls = 4;
        highFartingTime = new RangeAttribute(1, 3);
        lowFartingTime = new RangeAttribute(2, 4);
        lowHeight = 0.6f;

        attack.actDuration = 0.5f;
        attack.cd = 2f;
        attack.damage = 1.5f;
        attack.range = 10f;
    }
}

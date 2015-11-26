using UnityEngine;

public class WaspSet
{
    public Vector2 hpBarOffset = new Vector2(0, 4.5f);

    public float hp = 5;
    public SkillSetting attack = new SkillSetting();
    public RangeAttribute highFartingTime;
    public RangeAttribute lowFartingTime;

    protected WaspSet()
    {
        attack.actDuration = 0.5f;
        attack.cd = 2f;
        attack.damage = 1.5f;
        attack.range = 10f;
    }
}

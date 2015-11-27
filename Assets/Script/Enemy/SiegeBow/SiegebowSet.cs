using UnityEngine;

public class SiegeBowSet : CharacterSet
{

    public Vector2 hpBarOffset = new Vector2(0, 4.5f);

    public SkillSetting SiegeBowShoot = new SkillSetting();
    public float minShootRange = 1f;
    public float maxShootRange = 10f;

    public SiegeBowSet()
    {
        hp = 20;

        SiegeBowShoot.actDuration = 0.5f;
        SiegeBowShoot.cd = 2f;
        SiegeBowShoot.damage = 1.5f;
        SiegeBowShoot.range = 10f;
    }
}

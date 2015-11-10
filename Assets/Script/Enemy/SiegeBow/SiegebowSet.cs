using UnityEngine;

public class SiegeBowSet : Singleton<SiegeBowSet> {

    public Vector2 hpBarOffset = new Vector2(0, 1.5f);

    public float hp = 20;
    public SkillSetting SiegeBowShoot = new SkillSetting();
    public float minShootRange = 1f;

    protected SiegeBowSet()
    {
        SiegeBowShoot.actDuration = 0.5f;
        SiegeBowShoot.cd = 2f;
        SiegeBowShoot.demage = 1.5f;
        SiegeBowShoot.range = 10f;
    }
}

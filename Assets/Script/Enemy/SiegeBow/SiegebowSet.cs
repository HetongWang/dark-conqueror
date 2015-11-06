using UnityEngine;

public class SiegeBowSet : Singleton<SiegeBowSet> {

    public Vector2 hpBarOffset = new Vector2(0, 1.5f);

    public float hp = 20;
    public SkillSetting SiegeBowShoot = new SkillSetting(0.5f, 2f, 1.5f, 10f, 0);
}

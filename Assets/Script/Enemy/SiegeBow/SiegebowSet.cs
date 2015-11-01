using UnityEngine;

public class SiegeBowSet : Singleton<SiegeBowSet> {

    public float hp = 20;
    public SkillSetting.skill SiegeBowShoot = new SkillSetting.skill(0.5f, 2f, 1.5f, 10f, 0);
}

using UnityEngine;

public class PlayerSet : Singleton<PlayerSet> {

    public float hp = 50;
    public SkillSetting.skill NormalAttack = new SkillSetting.skill(0.40f, 0.45f, 1, 1f);
}

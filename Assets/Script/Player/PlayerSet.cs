using UnityEngine;

public class PlayerSet : Singleton<PlayerSet> {

    public float hp = 50;
    public float dashSpeed = 2f;
    public float dodgingForce = 520f;
    public SkillSetting.skill NormalAttack = new SkillSetting.skill(0.40f, 0.45f, 1f, 1f);
    public SkillSetting.skill Dodge = new SkillSetting.skill(0.8f, 1f, 0f, 2f);
}

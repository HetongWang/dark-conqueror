using UnityEngine;

public class PlayerSet : Singleton<PlayerSet> {

    public float hp = 50;
    public float dashSpeed = 2f;
    public float dodgingForce = 520f;
    public SkillSetting NormalAttack = new SkillSetting();
    public SkillSetting Dodge = new SkillSetting();

    protected PlayerSet()
    {
        NormalAttack.actDuration = 0.4f;
        NormalAttack.cd = 0.45f;
        NormalAttack.demage = 1f;
        NormalAttack.range = 1.4f;
        NormalAttack.attackDuration = 0.5f;
        NormalAttack.freezenTime = 0.5f;

        Dodge.actDuration = 0.8f;
        Dodge.cd = 1f;
        Dodge.demage = 0f;
        Dodge.range = 2f;
    }
}

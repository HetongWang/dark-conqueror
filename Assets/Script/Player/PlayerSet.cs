using UnityEngine;
using System.Collections.Generic;

public class PlayerSet : Singleton<PlayerSet> {

    public float hp = 5000;
    public float dashSpeed = 2f;
    public float dodgingForce = 520f;
    public List<SkillSetting> NormalAttack = new List<SkillSetting>();
    public BurnStatus.Setting normalAttackBurn = new BurnStatus.Setting();
    public SkillSetting Dodge = new SkillSetting();

    protected PlayerSet()
    {
        SkillSetting normalAttack1 = new SkillSetting();
        normalAttack1.actDuration = SkillSetting.frameToSeconds(13, 30);
        normalAttack1.damage = 1f;
        normalAttack1.range = 1.4f;
        normalAttack1.attackDuration = 0.1f;
        normalAttack1.freezenTime = 0.5f;
        NormalAttack.Add(normalAttack1);

        SkillSetting normalAttack2 = new SkillSetting();
        normalAttack2.actDuration = SkillSetting.frameToSeconds(13, 30);
        normalAttack2.damage = 1f;
        normalAttack2.range = 1.4f;
        normalAttack2.attackDuration = 0.1f;
        normalAttack2.freezenTime = 0.5f;
        NormalAttack.Add(normalAttack2);

        SkillSetting normalAttack3 = new SkillSetting();
        normalAttack3.actDuration = SkillSetting.frameToSeconds(15, 30);
        normalAttack3.damage = 1f;
        normalAttack3.range = 1.4f;
        normalAttack3.attackDuration = 0.1f;
        normalAttack3.freezenTime = 0.5f;
        NormalAttack.Add(normalAttack3);

        normalAttackBurn.damage = 10f;
        normalAttackBurn.duration = 3f;
        normalAttackBurn.maxOverlay = 1;

        Dodge.actDuration = 0.8f;
        Dodge.cd = 1f;
        Dodge.damage = 0f;
        Dodge.range = 2f;
    }
}

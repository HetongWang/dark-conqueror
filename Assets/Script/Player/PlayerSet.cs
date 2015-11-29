using UnityEngine;
using System.Collections.Generic;

public class PlayerSet : CharacterSet {

    public float dashSpeed = 2f;
    public float dodgingForce = 520f;
    public float dodgingSkyForce = 200f;
    public List<SkillSetting> NormalAttack = new List<SkillSetting>();
    public SkillSetting overheadSwing = new SkillSetting();
    public SkillSetting dodge = new SkillSetting();
    public SkillSetting block = new SkillSetting();
    public SkillSetting dropAttack = new SkillSetting();
    public float dropAttackForce = 50f;
    public BurnStatus.Setting normalAttackBurn = new BurnStatus.Setting();

    public PlayerSet()
    {
        hp = 200f;

        SkillSetting normalAttack1 = new SkillSetting();
        normalAttack1.actDuration = SkillSetting.frameToSeconds(14, 30);
        normalAttack1.damage = 1f;
        normalAttack1.range = 1.4f;
        normalAttack1.attackDuration = 0.1f;
        normalAttack1.freezenTime = 0.15f;
        normalAttack1.targetForce = new Vector2(50, 0);
        NormalAttack.Add(normalAttack1);

        SkillSetting normalAttack2 = new SkillSetting();
        normalAttack2.actDuration = SkillSetting.frameToSeconds(13, 30);
        normalAttack2.damage = 1f;
        normalAttack2.range = 1.4f;
        normalAttack2.attackDuration = 0.1f;
        normalAttack2.freezenTime = 0.1f;
        normalAttack2.targetForce = new Vector2(50, 0);
        NormalAttack.Add(normalAttack2);

        SkillSetting normalAttack3 = new SkillSetting();
        normalAttack3.actDuration = SkillSetting.frameToSeconds(20, 30);
        normalAttack3.damage = 1f;
        normalAttack3.range = 1.4f;
        normalAttack3.attackDuration = 0.1f;
        normalAttack3.freezenTime = 0.2f;
        normalAttack3.targetForce = new Vector2(150, 0);
        NormalAttack.Add(normalAttack3);

        normalAttackBurn.damage = 10f;
        normalAttackBurn.duration = 3f;
        normalAttackBurn.maxOverlay = 1;

        block.actDuration = float.PositiveInfinity;

        dodge.actDuration = 0.8f;
        dodge.cd = 1f;
        dodge.damage = 0f;
        dodge.range = 2f;

        overheadSwing.actDuration = SkillSetting.frameToSeconds(50, 30);
        overheadSwing.cd = 2f;
        overheadSwing.damage = 4f;
        overheadSwing.range = 1.5f;
        overheadSwing.freezenTime = 2f;
        overheadSwing.targetForce = new Vector2(200, 0);

        dropAttack.actDuration = 0.7f;
        dropAttack.cd = 2f;
        dropAttack.damage = 2f;
        dropAttack.range = 1f;
        dropAttack.attackDuration = 0.1f;
        dropAttack.freezenTime = 1f;
    }
}

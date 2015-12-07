using UnityEngine;
using System.Collections.Generic;

public class PlayerSet : CharacterSet {

    public float stamina;
    public float staminaRecoverSpeed;
    public float magic;
    public float magicRecoverSpeed;
    public float magicHitRecover;

    public float blockCost;
    public float dashSpeed = 2f;
    public float dashCost;
    public float dodgingForce = 520f;
    public float dodgingSkyForce = 200f;
    public float dodgeCost;
    public List<SkillSetting> normalAttack = new List<SkillSetting>();
    public SkillSetting overheadSwing = new SkillSetting();
    public SkillSetting dodge = new SkillSetting();
    public SkillSetting block = new SkillSetting();
    public SkillSetting dropAttack = new SkillSetting();
    public float dropAttackForce = 50f;
    public SkillSetting eruptionFire = new SkillSetting();
    public float eruptionFireCost;
    public int eruptionFireTimes;
    public BurnStatus.Setting normalAttackBurn = new BurnStatus.Setting();
    public BurnStatus.Setting eruptionFireBurn = new BurnStatus.Setting();

    public PlayerSet()
    {
        hp = 100f;

        stamina = 100f;
        staminaRecoverSpeed = 10f;
        dashCost = 20f;
        blockCost = 15f;
        dodgeCost = 20f;

        magic = 100f;
        magicRecoverSpeed = 0.3f;
        magicHitRecover = 3f;
        souls = 0;

        SkillSetting normalAttack1 = new SkillSetting();
        normalAttack1.actDuration = SkillSetting.frameToSeconds(14, 30);
        normalAttack1.damage = 1f;
        normalAttack1.range = 1.4f;
        normalAttack1.attackDuration = 0.1f;
        normalAttack1.freezenTime = 0.15f;
        normalAttack1.targetForce = new Vector2(50, 0);
        normalAttack1.name = "normalAttack";
        normalAttack.Add(normalAttack1);

        SkillSetting normalAttack2 = new SkillSetting();
        normalAttack2.actDuration = SkillSetting.frameToSeconds(13, 30);
        normalAttack2.damage = 1f;
        normalAttack2.range = 1.4f;
        normalAttack2.attackDuration = 0.1f;
        normalAttack2.freezenTime = 0.1f;
        normalAttack2.targetForce = new Vector2(50, 0);
        normalAttack2.name = "normalAttack";
        normalAttack.Add(normalAttack2);

        SkillSetting normalAttack3 = new SkillSetting();
        normalAttack3.actDuration = SkillSetting.frameToSeconds(20, 30);
        normalAttack3.damage = 1f;
        normalAttack3.range = 1.4f;
        normalAttack3.attackDuration = 0.1f;
        normalAttack3.freezenTime = 0.2f;
        normalAttack3.targetForce = new Vector2(150, 0);
        normalAttack3.name = "normalAttack";
        normalAttack.Add(normalAttack3);

        normalAttackBurn.damage = 1.5f;
        normalAttackBurn.duration = 2f;
        normalAttackBurn.maxOverlay = 1;

        block.actDuration = float.PositiveInfinity;

        dodge.actDuration = 0.4f;
        dodge.damage = 0f;
        dodge.range = 2f;

        overheadSwing.actDuration = SkillSetting.frameToSeconds(40, 30);
        overheadSwing.damage = 4f;
        overheadSwing.range = 1.5f;
        overheadSwing.cd = 1.3f;
        overheadSwing.freezenTime = 1f;
        overheadSwing.targetForce = new Vector2(200, 0);
        overheadSwing.name = "overheadSwing";

        dropAttack.actDuration = 0.7f;
        dropAttack.damage = 2f;
        dropAttack.range = 1f;
        dropAttack.attackDuration = 0f;
        dropAttack.freezenTime = 0.5f;
        dropAttack.name = "dropAttack";

        eruptionFireTimes = 2;
        eruptionFire.actDuration = 0.67f;
        eruptionFire.damage = 2f;
        eruptionFire.range = 1f;
        eruptionFire.attackDuration = 0.55f;
        eruptionFire.freezenTime = 0.6f;
        eruptionFireCost = 30f;

        eruptionFireBurn.damage = 2f;
        eruptionFireBurn.duration = 2f;
        eruptionFireBurn.maxOverlay = 1;
    }
}

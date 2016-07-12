using UnityEngine;

public class KittySet: CharacterSet {

    public Vector2 hpBarOffset = new Vector2(0, 4.5f);

    public float enrageTrigger;
    public float enrageEnhancement = 1.5f;

    public SkillSetting idle = new SkillSetting();
    public FloatRange idleTime;
    public float idleProAfterLeap;
    public FloatRange idleTimeAfterShadow;
    public SkillSetting KittyThrust = new SkillSetting();
    public SkillSetting KittyEnrage = new SkillSetting();
    public SkillSetting SummonWolf = new SkillSetting();
    public SkillSetting Leap = new SkillSetting();
    public float LeapAngle;
    public SkillSetting slash = new SkillSetting();
    public float slashMoveDist;
    public float slashForce;
    public SkillSetting shadowAttack = new SkillSetting();
    public float shadowAttackFadeOutTime;
    public IntRange shadowAttackTimes;
    public float shadowFadeOutTime;
    public float shadowFadeInTime;
    public FloatRange shadowSilenceTime;
    public Vector2 shadowPosition;
    public float shadowAttackSpeed;

    public float KittyWolfMoveSpeed = 11f;
    public float KittyWolfDistance = 30f;
    public float KittyWolfHP = 2f;

    public KittySet()
    {
        hp = 70;
        souls = 20;

        idleTime = new FloatRange(1f, 2f);

        KittyThrust.actDuration = SkillSetting.frameToSeconds(7, 12);
        KittyThrust.cd = 5f;
        KittyThrust.damage = 2f;
        KittyThrust.range = 1.4f;
        KittyThrust.attackDuration = 0.2f;
        KittyThrust.freezenTime = 0.8f;
        KittyThrust.targetForce = new Vector2(500, 0);

        enrageTrigger = hp * 0.7f;
        KittyEnrage.actDuration = 1f;
        KittyEnrage.cd = 2000f;
        KittyEnrage.damage = 0f;
        KittyEnrage.range = 5f;
        KittyEnrage.targetForce = new Vector2(100, 0);

        SummonWolf.actDuration = SkillSetting.frameToSeconds(7, 12);
        SummonWolf.cd = 18f;
        SummonWolf.damage = 3f;
        SummonWolf.range = 10f;
        SummonWolf.attackDuration = 0f;
        SummonWolf.freezenTime = 0.3f;

        Leap.actDuration = SkillSetting.frameToSeconds(13, 24);
        Leap.cd = 2f;
        Leap.range = 2f;
        LeapAngle = 45f;       
        idleProAfterLeap = 0.7f;

        slash.actDuration = SkillSetting.frameToSeconds(41, 40);
        slash.cd = 7f;
        slash.damage = 1.5f;
        slash.range = 1.3f;
        slash.freezenTime = 0.5f;
        slash.attackDuration = 0.2f;
        slashMoveDist = 2.5f;
        slash.targetForce = new Vector2(300f, 0);

        shadowAttack.actDuration = float.PositiveInfinity;
        shadowAttack.damage = 5f;
        shadowAttack.cd = 25f;
        shadowAttack.freezenTime = 1f;
        shadowAttack.attackDuration = 0;
        shadowAttack.targetForce = new Vector2(400f, 0);

        shadowFadeOutTime = 1f;
        shadowFadeInTime = 1f;
        shadowSilenceTime = new FloatRange(1, 3);
        shadowAttackTimes = new IntRange(2, 4);
        shadowAttackFadeOutTime = 0.3f;
        shadowPosition = new Vector2(6, 4);
        shadowAttackSpeed = 18f;
        idleTimeAfterShadow = new FloatRange(4, 6);
    }
}

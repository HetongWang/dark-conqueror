using UnityEngine;

public class KittySet: CharacterSet {

    public Vector2 hpBarOffset = new Vector2(0, 4.5f);

    public float enrageTrigger;
    public float enrageEnhancement = 1.5f;

    public SkillSetting KittyThrust = new SkillSetting();

    public SkillSetting KittyEnrage = new SkillSetting();
    public SkillSetting SummonWolf = new SkillSetting();
    public SkillSetting Leap = new SkillSetting();
    public float LeapAngle;
    public SkillSetting slash = new SkillSetting();
    public float slashMoveDist;
    public float slashForce;

    public float KittyWolfMoveSpeed = 11f;
    public float KittyWolfDistance = 30f;
    public float KittyWolfHP = 2f;

    public KittySet()
    {
        hp = 50;

        KittyThrust.actDuration = SkillSetting.frameToSeconds(7, 12);
        KittyThrust.cd = 3f;
        KittyThrust.damage = 2f;
        KittyThrust.range = 1.4f;
        KittyThrust.attackDuration = 0.6f;
        KittyThrust.freezenTime = 0.8f;
        KittyThrust.targetForce = new Vector2(500, 0);

        enrageTrigger = hp * 0.9f;
        KittyEnrage.actDuration = 1f;
        KittyEnrage.cd = 2000f;
        KittyEnrage.damage = 0f;
        KittyEnrage.range = 5f;
        KittyEnrage.targetForce = new Vector2(100, 0);

        SummonWolf.actDuration = SkillSetting.frameToSeconds(7, 12);
        SummonWolf.cd = 8f;
        SummonWolf.damage = 3f;
        SummonWolf.range = 10f;
        SummonWolf.attackDuration = 0f;
        SummonWolf.freezenTime = 0.3f;

        Leap.actDuration = SkillSetting.frameToSeconds(13, 24);
        Leap.cd = 2f;
        Leap.range = 2f;
        LeapAngle = 45f;       

        slash.actDuration = SkillSetting.frameToSeconds(41, 40);
        slash.cd = 2f;
        slash.damage = 1.5f;
        slash.range = 1.3f;
        slash.freezenTime = 0.5f;
        slash.attackDuration = 0.2f;
        slashMoveDist = 2.5f;
        slash.targetForce = new Vector2(300f, 0);
    }
}

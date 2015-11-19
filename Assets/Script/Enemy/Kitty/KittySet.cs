using UnityEngine;

public class KittySet: Singleton<KittySet> {

    public Vector2 hpBarOffset = new Vector2(0, 1.5f);

    public float hp = 50;
    public float enrageTrigger = 47f;
    public float enrageEnhancement = 1.5f;

    public SkillSetting KittyThrust = new SkillSetting();

    public SkillSetting KittyEnrage = new SkillSetting();
    public SkillSetting SummonWolf = new SkillSetting();
    public SkillSetting Leap = new SkillSetting();
    public float LeapAngle;
    public SkillSetting Slash = new SkillSetting();
    public float slashMoveDist;
    public float slashForce;

    public float KittyWolfMoveSpeed = 11f;
    public float KittyWolfDistance = 30f;
    public float KittyWolfHP = 2f;

    protected KittySet()
    {
        KittyThrust.actDuration = SkillSetting.frameToSeconds(7, 12);
        KittyThrust.cd = 3f;
        KittyThrust.damage = 2f;
        KittyThrust.range = 1.4f;
        KittyThrust.attackDuration = 0.6f;
        KittyThrust.freezenTime = 0.8f;
        KittyThrust.targetForce = new Vector2(500, 0);

        KittyEnrage.actDuration = 1f;
        KittyEnrage.cd = 2000f;
        KittyEnrage.damage = 0f;
        KittyEnrage.range = 5f;

        SummonWolf.actDuration = SkillSetting.frameToSeconds(7, 12);
        SummonWolf.cd = 8f;
        SummonWolf.damage = 3f;
        SummonWolf.range = 10f;
        SummonWolf.attackDuration = 0f;

        Leap.actDuration = SkillSetting.frameToSeconds(13, 24);
        Leap.cd = 2f;
        Leap.range = 2f;
        LeapAngle = 45f;       

        Slash.actDuration = SkillSetting.frameToSeconds(41, 40);
        Slash.cd = 2f;
        Slash.damage = 1.5f;
        Slash.range = 1.3f;
        Slash.freezenTime = 0.5f;
        Slash.attackDuration = 0.5f;
        slashMoveDist = 2.5f;
        Slash.targetForce = new Vector2(300f, 0);
    }
}

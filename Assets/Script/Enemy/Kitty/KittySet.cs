using UnityEngine;

public class KittySet: Singleton<KittySet> {

    public Vector2 hpBarOffset = new Vector2(0, 1.5f);

    public float hp = 50;
    public float enrageTrigger = 47f;
    public float enrageEnhancement = 1.5f;

    public SkillSetting KittyThrust = new SkillSetting();
    public float KittyThrustForceIntensity = 50f;

    public SkillSetting KittyEnrage = new SkillSetting();
    public SkillSetting SummonWolf = new SkillSetting();

    public float KittyWolfMoveSpeed = 11f;
    public float KittyWolfDistance = 30f;
    public float KittyWolfHP = 2f;

    protected KittySet()
    {
        KittyThrust.actDuration = 0.6f;
        KittyThrust.cd = 2f;
        KittyThrust.damage = 2f;
        KittyThrust.range = 1.4f;
        KittyThrust.attackDuration = 0.6f;
        KittyThrust.freezenTime = 0.8f;

        KittyEnrage.actDuration = 1f;
        KittyEnrage.cd = 2000f;
        KittyEnrage.damage = 0f;
        KittyEnrage.range = 5f;

        SummonWolf.actDuration = 0.6f;
        SummonWolf.cd = 8f;
        SummonWolf.damage = 3f;
        SummonWolf.range = 10f;
        SummonWolf.attackDuration = 0f;
    }
}

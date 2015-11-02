using UnityEngine;

public class KittySet: Singleton<KittySet> {

    public float hp = 50;
    public float enrageTrigger = 47f;
    public float enrageEnhancement = 1.5f;
    public SkillSetting.skill KittyThrust = new SkillSetting.skill(0.6f, 2f, 2f, 1.2f, 0.6f);
    public SkillSetting.skill KittyEnrage = new SkillSetting.skill(1f, 200f, 0f, 5f);
    public SkillSetting.skill SummonWolf = new SkillSetting.skill(0.6f, 8f, 3f, 10f, 0);

    public float KittyWolfMoveSpeed = 5f;
    public float KittyWolfHP = 2f;
}

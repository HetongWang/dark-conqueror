using UnityEngine;

public class KittySet: Singleton<KittySet> {

    public float hp = 50;
    public float enrageTrigger = 49f;
    public SkillSetting.skill KittyThrust = new SkillSetting.skill(0.6f, 2f, 2f, 1.2f);
    public SkillSetting.skill KittyEnrage = new SkillSetting.skill(1f, 200f, 0f, 5f);
}

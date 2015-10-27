using UnityEngine;
using System.Collections.Generic;

public class SkillSetting : Singleton<SkillSetting> {

    public struct skill
    {
        public float cd, duration, demage, range;

        public skill(float duration, float cd, float demage, float range)
        {
            this.duration = duration;
            this.cd = cd;
            this.demage = demage;
            this.range = range;
        }
    }

    public skill KittyThrust = new skill(0.6f, 2f, 2f, 2f);
    public skill SiegeBowShoot = new skill(0, 2f, 1.5f, 10f);
    public skill NormalAttack = new skill(0.5f, 0.5f, 1, 2f);
}

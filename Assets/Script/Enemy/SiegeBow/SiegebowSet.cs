using UnityEngine;

public class SiegeBowSet : EnemySet
{
    public SkillSetting SiegeBowShoot = new SkillSetting();
    public float minShootRange = 1f;
    public float maxShootRange = 10f;
    public float shootAngle;

    public SiegeBowSet()
    {
        hp = 20;
        souls = 3;

        SiegeBowShoot.actDuration = 0.5f;
        SiegeBowShoot.cd = 2f;
        SiegeBowShoot.damage = 1.5f;
        SiegeBowShoot.range = 10f;
        SiegeBowShoot.freezenTime = 0.5f;
        shootAngle = 60f * Mathf.Deg2Rad;
    }
}

using UnityEngine;

public class RotopollySet : Singleton<RotopollySet>
{
    public Vector2 hpBarOffset = new Vector2(0, 4.5f);

    public float hp = 3;
    public float moveSpeed = 2f;
    public SkillSetting run = new SkillSetting();
    public float runSpeed = 10f;
    public float runAcceleration = 12f;
    public SkillSetting jump = new SkillSetting();

    protected RotopollySet()
    {
        run.actDuration = 1f;
        run.cd = 8f;
        run.range = 9f;
        run.damage = 2f;
        run.attackDuration = 0f;
        run.freezenTime = 0.3f;
        run.targetForce = new Vector2(100, 0);

        jump.actDuration = 0f;
        jump.cd = 2f;
        jump.selfForce = new Vector2(50, 300);
    }
}
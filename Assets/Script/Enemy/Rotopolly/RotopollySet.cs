using UnityEngine;

public class RotopollySet : CharacterSet
{
    public Vector2 hpBarOffset = new Vector2(0, 4.5f);

    public SkillSetting run = new SkillSetting();
    public float runSpeed = 10f;
    public float runAcceleration = 12f;
    public float runComboChance = 0.3f;
    public SkillSetting jump = new SkillSetting();

    public RotopollySet()
    {
        hp = 3;
        moveSpeed = 2f;

        run.actDuration = 1f;
        run.cd = 6f;
        run.range = 9f;
        run.damage = 2f;
        run.attackDuration = 0f;
        run.freezenTime = 0.3f;
        run.targetForce = new Vector2(200, 0);

        jump.actDuration = 0f;
        jump.cd = 2f;
        jump.selfForce = new Vector2(50, 300);
    }
}
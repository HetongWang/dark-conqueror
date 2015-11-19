using UnityEngine;

public class CatWolfSet : Singleton<CatWolfSet>
{
    public Vector2 hpBarOffset = new Vector2(0, 1.5f);
    public int amount = 3;

    public float hp = 10;
    public float moveSpeed = 8f;

    public SkillSetting alert = new SkillSetting();
    public SkillSetting maul = new SkillSetting();
    public SkillSetting pounce = new SkillSetting();
    public SkillSetting summonFriends = new SkillSetting();
    public float summonFriendsInitCD;
    public SkillSetting crouch = new SkillSetting();

    public float huddleRatio = 0.5f;

    protected CatWolfSet()
    {
        alert.actDuration = 1f;
        alert.cd = 2f;

        maul.actDuration = SkillSetting.frameToSeconds(16, 12);
        maul.cd = 2f;
        maul.damage = 1f;
        maul.range = 1.3f;
        maul.freezenTime = 0.7f;

        pounce.actDuration = 1f;
        pounce.cd = 3f;
        pounce.damage = 2f;
        pounce.range = 4f;
        pounce.freezenTime = 0.2f;
        pounce.attackDuration = 0.9f;
        pounce.selfForce = new Vector2(350, 350);
        
        summonFriends.actDuration = 0.6f;
        summonFriends.cd = 6f;
        summonFriends.attackDuration = 0f;
        summonFriendsInitCD = 8f;

        crouch.actDuration = 1.25f;
        crouch.cd = 2f;
        crouch.damage = 0.3f; // damage reduced
    }
}

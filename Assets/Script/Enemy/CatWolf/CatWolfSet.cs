﻿using UnityEngine;

public class CatWolfSet : Singleton<CatWolfSet>
{
    public Vector2 hpBarOffset = new Vector2(0, 1.5f);

    public float hp = 50;
    public float moveSpeed = 8f;

    public SkillSetting alert = new SkillSetting();
    public SkillSetting maul = new SkillSetting();
    public SkillSetting pounce = new SkillSetting();
    public Vector2 pounceForce;
    public SkillSetting summonFriends = new SkillSetting();
    public float summonFriendsInitCD;
    public SkillSetting crouch = new SkillSetting();

    public float huddleRatio = 0.5f;

    protected CatWolfSet()
    {
        alert.actDuration = 1.25f;
        alert.cd = 2f;

        maul.actDuration = SkillSetting.frameToSeconds(15, 12);
        maul.cd = 2f;
        maul.damage = 1f;
        maul.range = 1f;
        maul.freezenTime = 0.2f;

        pounce.actDuration = 0.9f;
        pounce.cd = 3f;
        pounce.damage = 2f;
        pounce.range = 4f;
        pounce.freezenTime = 0.5f;
        pounce.attackDuration = 0.9f;
        pounceForce = new Vector2(350, 350);
        
        summonFriends.actDuration = 0.6f;
        summonFriends.cd = 8f;
        summonFriends.attackDuration = 0f;
        summonFriendsInitCD = 10f;

        crouch.actDuration = 1.25f;
        crouch.cd = 2f;
        crouch.damage = 0.3f; // damage reduced
    }
}

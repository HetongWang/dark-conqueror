using UnityEngine;
using System.Collections.Generic;

public class SkillSetting : Singleton<SkillSetting> {
    public Dictionary<string, float> kitty = new Dictionary<string, float>();

    void Awake()
    {
        kitty.Add("duration", 0.6f);
        kitty.Add("cd", 2f);
        kitty.Add("demage", 2f);
    }
}

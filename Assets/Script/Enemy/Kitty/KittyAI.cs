using UnityEngine;
using System.Collections.Generic;

public class KittyAI : BasicAI {

    private bool enraged = false;
    protected Kitty person;

    public KittyAI(Enemy kitty): base(kitty)
    {
        viewRange = 15f;
        person = (Kitty)kitty;
    }

    public string attack()
    {
        string skillName = null;

        if (isEnrage())
            return "enrage";

        if (person.enraged && person.skillCooler["shadow"] <= 0)
            return "shadow";

        if (targetPlayerDistance < person.setting.SummonWolf.range && person.skillCooler["summonWolf"] <= 0)
            return "summonWolf";

        if (targetPlayer)
        {
            float dis = Mathf.Abs(targetPlayer.transform.position.x - person.transform.position.x);
            if (dis < 1.5)
            {
                skillName = "leap";
                int r = (int)(Random.value * 3);
                if (r == 0)
                {
                    if (person.skillCooler["thrust"] <= 0)
                        skillName = "thrust";
                }
                else if (r == 1)
                {
                    if (person.skillCooler["slash"] <= 0)
                        skillName = "slash";
                }
                else if (r == 2)
                {
                    if (person.skillCooler["leap"] <= 0)
                        skillName = "leap";
                }

            }
        }
        return skillName;
    }

    bool isEnrage()
    {
        bool res = false;
        if (!enraged && person.hp < person.setting.enrageTrigger)
        {
            res = true;
            enraged = true;
        }
        return res;
    }

    public override string update()
    {
        seekPlayer();
        string skill = attack();
        if (skill != null)
            return skill;
        else
        {
            if (targetPlayerDistance <= person.setting.KittyThrust.range)
            {
                return "idle";
            }
            else
                return "move";
        }
    }
}

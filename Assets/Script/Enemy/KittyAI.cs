using UnityEngine;
using System.Collections.Generic;

public class KittyAI : BasicAI {

    public KittyAI(Character kitty): base(kitty)
    {

    }

    public string attack()
    {
        string skillName = null;

        if (isEnrage())
            return "enrage";

        if (targetPlayer)
        {
            float dis = Mathf.Abs(targetPlayer.transform.position.x - person.transform.position.x);
            if (dis < KittySet.Instance.KittyThrust.range && person.skillCooler["thrust"] <= 0)
            {
                skillName = "thrust";
            }
        }
        return skillName;
    }

    bool isEnrage()
    {
        if (person.hp < 10)
            return true;
        else
            return false;
    }
}

using UnityEngine;
using System.Collections.Generic;

public class KittyAI : BasicAI {

    private bool enraged = false;

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
        bool res = false;
        if (!enraged && person.hp < KittySet.Instance.enrageTrigger)
        {
            res = true;
            enraged = true;
        }
        return res;
    }
}

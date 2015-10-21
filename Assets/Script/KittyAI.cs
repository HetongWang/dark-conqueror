using UnityEngine;
using System.Collections.Generic;

public class KittyAI : BasicAI {

    public KittyAI(Character kitty): base(kitty)
    {

    }

    public string attack()
    {
        string skillName = null;
        if (targetPlayer)
        {
            float dis = Mathf.Abs(targetPlayer.transform.position.x - person.transform.position.x);
            if (dis < KittyThrustAttack.Range && person.skillCooler["thrust"] <= 0)
            {
                skillName = "thrust";
            }
        }
        return skillName;
    }
}

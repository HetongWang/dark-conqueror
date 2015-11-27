using UnityEngine;
using System.Collections.Generic;

public class CatWolfAI : BasicAI
{
    public bool alerted = false;
    public CatWolf person;

    public CatWolfAI(Enemy catwolf) : base(catwolf)
    {
        viewRange = 15f;
        distantToPlayer = 1.3f;
        person = (CatWolf)_person;
    }

    public override string update()
    {
        seekPlayer();
        if (movementMode == moveMode.guard ||
            person.facingRight != targetOnRight)
            return "move";

        if (person.skillCooler["summonFriends"] <= 0 && CatWolf.amount < CatWolfSet.amount)
            return "summonFriends";

        if (targetPlayerDistance > person.setting.maul.range && 
            targetPlayerDistance < person.setting.pounce.range && 
            person.skillCooler["pounce"] <= 0)
        {
            if (alerted)
                return "pounce";
            else
                return "alert";
        }

        if (targetPlayerDistance < person.setting.maul.range && person.skillCooler["maul"] <= 0)
        {
            if (alerted)
                return "maul";
            else
                return "alert";
        }

        //if (targetPlayerDistance < person.setting.pounce.range)
        //   return "crouch";

        return "move";
    }
}
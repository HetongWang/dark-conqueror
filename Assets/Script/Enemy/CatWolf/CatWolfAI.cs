using UnityEngine;
using System.Collections.Generic;

public class CatWolfAI : BasicAI
{
    public bool alerted = false;

    public CatWolfAI(Enemy catwolf) : base(catwolf)
    {
        viewRange = 15f;
        distantToPlayer = 1.3f;
    }

    public override string update()
    {
        seekPlayer();
        if (movementMode == moveMode.guard ||
            person.facingRight != targetOnRight)
            return "move";

        if (person.skillCooler["summonFriends"] <= 0 && CatWolf.amount < CatWolfSet.Instance.amount)
            return "summonFriends";

        if (targetPlayerDistance > CatWolfSet.Instance.maul.range && 
            targetPlayerDistance < CatWolfSet.Instance.pounce.range && 
            person.skillCooler["pounce"] <= 0)
        {
            if (alerted)
                return "pounce";
            else
                return "alert";
        }

        if (targetPlayerDistance < CatWolfSet.Instance.maul.range && person.skillCooler["maul"] <= 0)
        {
            if (alerted)
                return "maul";
            else
                return "alert";
        }

        //if (targetPlayerDistance < CatWolfSet.Instance.pounce.range)
        //   return "crouch";

        return "move";
    }
}
using UnityEngine;
using System.Collections.Generic;

public class CatWolfAI : BasicAI
{
    public bool alerted = false;

    public CatWolfAI(Character catwolf) : base(catwolf)
    {
        viewRange = 15f;
    }

    public override string update()
    {
        seekPlayer();
        if (movementMode == moveMode.guard)
            return "move";

        if (person.skillCooler["summonFriends"] <= 0)
            return "summonFriends";

        if (targetPlayerDistance < CatWolfSet.Instance.pounce.range && person.skillCooler["pounce"] <= 0)
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

        if (targetPlayerDistance < CatWolfSet.Instance.pounce.range)
            return "huddle";

        return "move";
    }
}
using UnityEngine;
using System.Collections.Generic;

public class CatWolfAI : BasicAI
{
    public CatWolfAI(Character catwolf) : base(catwolf)
    {
        viewRange = 15f;
    }

    public override string update()
    {

    }
}
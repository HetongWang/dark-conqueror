using UnityEngine;
using System.Collections.Generic;

public class EnemySet : CharacterSet
{
    public Vector2 hpBarOffset;

    public EnemySet()
    {
        hpBarOffset = new Vector2(0, 3.5f);
    }
}

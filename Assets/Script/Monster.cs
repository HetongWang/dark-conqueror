using UnityEngine;
using System.Collections;

public class Monster: Character
{
    public override void Awake()
    {
        base.Awake();
        hp = 3;
    }

    public override void Update()
    {
        base.Update();
        AliveOrDie();
    }
}
using UnityEngine;
using System.Collections;

public class MooControl : Character
{
    void Awake()
    {
        hp = 3;
    }

    void Update()
    {
        AliveOrDie();
    }
}
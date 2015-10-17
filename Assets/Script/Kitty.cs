using UnityEngine;
using System.Collections;

public class Kitty: Character
{
    private BasicAI ai;

    public override void Awake()
    {
        base.Awake();
        hp = 3;
        ai = new BasicAI(transform.position);
    }

    public override void Update()
    {
        base.Update();
    }

    void FixedUpdate()
    {

    }
}
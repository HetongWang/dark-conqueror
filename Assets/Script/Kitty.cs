using UnityEngine;
using System.Collections;

public class Kitty: Character
{
    private BasicAI ai;

    public override void Awake()
    {
        base.Awake();
        facingRight = false;
        hp = 30;
        ai = new BasicAI(transform.position);
    }

    public override void Update()
    {
        base.Update();
        AliveOrDie();
    }

    void FixedUpdate()
    {
        float horMove = ai.horMove(transform.position);
        move(horMove);
    }
}
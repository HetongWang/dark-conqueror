using UnityEngine;
using System.Collections.Generic;

public class RotopollyAI : BasicAI
{
    public float moveTimer = 0;
    public float idleTimer = 0;
    public float moveTime = 5f;
    public float idleTime = 2f;
    public float comboChance = 0.3f;

    public RotopollyAI(Enemy c) : base(c)
    {
        viewRange = 2f;
    }

    public override string update()
    {
        base.update();
        seekPlayer();
        switch (person.behavior)
        {
            case "run":
                runStatus();
                break;
            case "move":
                moveStatus();
                break;
            case "idle":
                idleStatus();
                break;
        }
        if (currentStatus == "move")
            moveTimer += Time.deltaTime;
        else
            moveTimer = 0;

        if (currentStatus == "idle")
            idleTimer += Time.deltaTime;
        else
            idleTimer = 0;

        return currentStatus;
    }

    public void idleStatus()
    {
        if (idleTimer > idleTime)
            currentStatus = "move";
    }

    public void moveStatus()
    {
        if (person.skillCooler["run"] <= 0 && targetPlayerDistance < RotopollySet.Instance.run.range)
        {
            float dis = targetPlayer.transform.position.x - person.transform.position.x;
            if (person.facingRight && dis < 0)
                person.Flip();
            if (!person.facingRight && dis > 0)
                person.Flip();
            currentStatus = "run";
        }

        if (moveTimer > moveTime)
            currentStatus = "idle";
    }

    public void runStatus()
    {
        float dis = targetPlayer.transform.position.x - person.transform.position.x;
        if (person.facingRight)
        {
            if (dis < 0)
            {
                if (Random.value > comboChance)
                    currentStatus = "idle";
                person.Flip();
            }
        }
        else
        {
            if (dis > 0)
            {
                if (Random.value > comboChance)
                    currentStatus = "idle";
                person.Flip();
            }
        }
    }
}
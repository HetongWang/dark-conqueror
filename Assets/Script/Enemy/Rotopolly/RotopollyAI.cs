using UnityEngine;
using System.Collections;

public class RotopollyAI : BasicAI
{
    public float moveTimer = 0;
    public float idleTimer = 0;
    public float moveTime = 4f;
    public float idleTime = 2f;
    public float comboChance = RotopollySet.Instance.runComboChance;

    public RotopollyAI(Enemy c) : base(c)
    {
        viewRange = 20f;
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
                else
                {
                    person.StartCoroutine(delayFlip());
                }
            }
        }
        else
        {
            if (dis > 0)
            {
                if (Random.value > comboChance)
                    currentStatus = "idle";
                else
                {
                    person.StartCoroutine(delayFlip());
                }
            }
        }
    }

    public IEnumerator delayFlip()
    {
        yield return new WaitForEndOfFrame();
        person.Flip();
        yield break;
    }

    protected override float attackMovement(GameObject player)
    {
        if (player == null)
            return 0;

        float res = 0;
        float dis = person.transform.position.x - player.transform.position.x;
        if (dis > 0)
            res = 1;
        else if (dis < 0)
            res = -1;
        return res;
    }
}
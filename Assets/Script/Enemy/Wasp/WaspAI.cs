using UnityEngine;

public class WaspAI : BasicAI
{
    public RangeAttribute highTimeRange;
    public Wasp person;
    public float timer;
    public float timeLimit;

    public WaspAI(Enemy wasp) : base(wasp)
    {
        currentStatus = "high";
        person = (Wasp)_person;
        timeLimit = person.setting.highFartingTime.random();
    }

    public override string update()
    {
        seekPlayer();
        switch (_person.behavior)
        {
            case "high":
                highStatus();
                break;
            case "low":
                lowStatus();
                break;
            case "attack":
                attackStatus();
                break;
        }
        return currentStatus;
    }

    public void highStatus()
    {
        timer += Time.deltaTime;
        if (timer > timeLimit && !person.inAction())
        {
            timer = 0;
            timeLimit = 0;
            currentStatus = "attack";
        }
    }

    public void lowStatus()
    {
        timer += Time.deltaTime;
        if (timer > timeLimit && !person.inAction())
        {
            timer = 0;
            timeLimit = person.setting.highFartingTime.random();
            currentStatus = "high";
        }

    }

    public void attackStatus()
    {
        timer += Time.deltaTime;
        if (timer > timeLimit && !person.inAction())
        {
            timer = 0;
            timeLimit = person.setting.lowFartingTime.random();
            currentStatus = "low";
        }
    }
}

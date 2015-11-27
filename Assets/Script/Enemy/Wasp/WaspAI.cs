using UnityEngine;

public class WaspAI : BasicAI
{
    public RangeAttribute highTimeRange;

    public WaspAI(Enemy wasp) : base(wasp)
    {
        currentStatus = "high";
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

    }

    public void lowStatus()
    {

    }

    public void attackStatus()
    {

    }
}

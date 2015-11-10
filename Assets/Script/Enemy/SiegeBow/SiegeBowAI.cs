using UnityEngine;

public class SiegeBowAI : BasicAI
{
    public float error = 0.2f;
    public float minShootRange = SiegeBowSet.Instance.minShootRange;
    public Vector2 velocity;

    public SiegeBowAI(Character siegebow) : base(siegebow)
    {

    }

    public Vector2 shootVelocity()
    {
        float h = -(person.transform.position.y - targetPlayer.transform.position.y);
        float d = person.transform.position.x - targetPlayer.transform.position.x;
        float g = Physics2D.gravity.y;

        d = Random.Range(1 - error, 1 + error) * d;

        SiegeBow bow = (SiegeBow)person;
        float vx = -d * Mathf.Sqrt(g / (2 * (h - Mathf.Tan(bow.angle) * d)));
        float vy = Mathf.Abs(Mathf.Tan(bow.angle) * vx);
        Vector2 v = new Vector2(vx, vy);

        return v;
    }

    public override string update()
    {
        seekPlayer();
        if (couldShoot())
        {
            velocity = shootVelocity();
            if (!float.IsNaN(velocity.x))
                return "shoot";
        }

        return null;
    }
    bool couldShoot()
    {
        bool res = true;
        if (person.skillCooler["shoot"] > 0)
            res = false;
        if (targetPlayer.transform.position.x - person.transform.position.x > -minShootRange)
            res = false;

        return res;
    }

}

using UnityEngine;

public class SiegeBowAI : BasicAI
{
    public float error = 1f;
    public float minShootRange;
    public float maxShootRange;
    public Vector2 velocity;

    private SiegeBow person;

    public SiegeBowAI(Enemy siegebow) : base(siegebow)
    {
        person = (SiegeBow)siegebow;
        minShootRange = person.setting.minShootRange;
        maxShootRange = person.setting.maxShootRange;
    }

    public Vector2 shootVelocity()
    {
        float h = -(_person.transform.position.y - targetPlayer.transform.position.y);
        float d = _person.transform.position.x - targetPlayer.transform.position.x;
        float g = Physics2D.gravity.y;

        d = Random.Range(-error, error) + d;

        SiegeBow bow = (SiegeBow)_person;
        float vx = -d * Mathf.Sqrt(g / (2 * (h - Mathf.Tan(bow.setting.shootAngle) * d)));
        float vy = Mathf.Abs(Mathf.Tan(bow.setting.shootAngle) * vx);
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
        if (_person.skillCooler["shoot"] > 0)
            res = false;
        if (targetPlayer.transform.position.x - _person.transform.position.x > -minShootRange ||
            targetPlayer.transform.position.x - _person.transform.position.x < -maxShootRange)
            res = false;

        return res;
    }

}

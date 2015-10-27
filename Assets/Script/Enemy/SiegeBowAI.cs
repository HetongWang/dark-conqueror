using UnityEngine;

public class SiegeBowAI : BasicAI
{
    public float angle = Mathf.Deg2Rad * 30;
    public float error = 0.2f;

    public SiegeBowAI(Character siegebow) : base(siegebow)
    {

    }

    public Vector2 shootVelocity()
    {
        float h = -(person.transform.position.y - targetPlayer.transform.position.y);
        float d = person.transform.position.x - targetPlayer.transform.position.x;
        float g = Physics2D.gravity.y;

        d = Random.Range(1 - error, 1 + error) * d;

        float vx = d * Mathf.Sqrt(g / (2 * (h - Mathf.Tan(angle) * d)));
        float vy = Mathf.Tan(angle) * vx;
        Vector2 v = new Vector2(vx, vy);

        Debug.Log(h - Mathf.Tan(angle) * d);

        return v;
    }
}

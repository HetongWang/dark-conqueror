using UnityEngine;
using System.Collections.Generic;

public class Souls : MonoBehaviour
{
    protected Rigidbody2D body;
    void Update()
    {
        body = GetComponent<Rigidbody2D>();
        if (Mathf.Abs(body.velocity.x) > 0.1f)
        {
            //float angle = Mathf.Abs(body.velocity.x) / body.velocity.magnitude * 90;
            //if (body.velocity.x < 0)
            //    transform.localRotation = Quaternion.Euler(0, 0, angle);
            //else
            //    transform.localRotation = Quaternion.Euler(0, 0, angle + 90);
            float angle = Mathf.Atan(body.velocity.y / body.velocity.x);
            angle = angle * Mathf.Rad2Deg;
            if (body.velocity.x < 0)
                transform.localRotation = Quaternion.Euler(0, 0, angle);
            else
                transform.localRotation = Quaternion.Euler(0, 0, angle + 180);
        }
        else
            transform.localRotation = Quaternion.Euler(0, 0, 90);
    }

    public void disappear()
    {
        Destroy(gameObject);
    }
}
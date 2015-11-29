using UnityEngine;
using System.Collections.Generic;

public class Souls : MonoBehaviour
{
    protected Rigidbody2D body;

    void Update()
    {
        body = GetComponent<Rigidbody2D>();
        if (body.velocity.x > 0)
        {
            float angle = Mathf.Atan(body.velocity.y / body.velocity.x) + 90;
            angle = angle * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void disappear()
    {
        Destroy(gameObject);
    }
}
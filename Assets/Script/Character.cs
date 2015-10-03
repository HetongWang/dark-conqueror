using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    [HideInInspector]
    public bool facingRight = true;

    public float moveForce = 300f;
    public float maxSpeed = 2f;

    public int hp = 10;

    protected void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x = scale.x * -1;
        transform.localScale = scale;
    }

    public void Hurt()
    {
        hp--;
    }
    public void Hurt(int amount)
    {
        hp -= amount;
    }
}

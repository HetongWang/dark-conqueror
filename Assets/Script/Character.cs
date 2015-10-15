using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    [HideInInspector]
    public bool facingRight = true;
    public float moveSpeed = 4f;

    public float jumpForce = 600;
    protected Transform groundCheck;
    protected bool grounded = false;

    public int hp = 10;
    protected Rigidbody2D body;

    public virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("groundCheck");
    }

    public virtual void Update()
    {
        // Check if grounded
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

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

    public void AliveOrDie()
    {
        if (hp <= 0)
            Destroy(gameObject);
    }

    public void move(float horInput)
    {
        // Control horizontal speed
        if (horInput != 0)
            body.velocity = new Vector2(Mathf.Sign(horInput) * moveSpeed, body.velocity.y);
        else
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }

        // Change facing direct
        if ((horInput > 0 && !facingRight) || (horInput < 0 && facingRight))
        {
            Flip();
        }
    }

    public void move(float horInput, float verInput)
    {
        move(horInput);
        if (verInput != 0)
            body.velocity = new Vector2(body.velocity.x, verInput * moveSpeed);
        else
        {
            body.velocity = new Vector2(body.velocity.x, 0);
        }
    }

    public void jump()
    {
        if (grounded)
            body.AddForce(new Vector2(0, jumpForce));
    }
}

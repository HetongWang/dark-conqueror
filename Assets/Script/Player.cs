using UnityEngine;
using System.Collections;

public class Player : Character 
{
    [HideInInspector]
    public bool jump = false;

    public float jumpForce = 500;

    private Transform groundCheck;
    private bool grounded = false;
    private Rigidbody2D rigidbody;

    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {
        groundCheck = transform.Find("groundCheck");
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if grounded
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        float horInput = Input.GetAxisRaw("Horizontal");

        // Control horizontal speed
        if (horInput != 0)
            rigidbody.velocity = new Vector2(Mathf.Sign(horInput) * maxSpeed, rigidbody.velocity.y);
        else
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        }

        // Change facing direct
        if ((horInput > 0 && !facingRight) || (horInput < 0 && facingRight))
        {
            Flip();
        }

        // Jump
        if (jump)
        {
            rigidbody.AddForce(new Vector2(0, jumpForce));
            jump = false;
        }
    }
}

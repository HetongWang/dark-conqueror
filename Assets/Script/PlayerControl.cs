using UnityEngine;
using System.Collections;

public class PlayerControl : Character 
{
    [HideInInspector]
    public bool jump = false;

    public float jumpForce = 500;

    private Transform groundCheck;
    private bool grounded = false;

    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {
        groundCheck = transform.Find("groundCheck");
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
        float horInput = Input.GetAxis("Horizontal");
        
        // Control horizontal speed
        if (horInput * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * horInput * moveForce);
        }
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        // Change facing direct
        if ((horInput > 0 && !facingRight) || (horInput < 0 && facingRight))
        {
            Flip();
        }

        // Jump
        if (jump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            jump = false;
        }
    }
}

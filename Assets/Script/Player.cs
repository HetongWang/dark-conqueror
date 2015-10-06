using UnityEngine;
using System.Collections;

public class Player : Character 
{
    [HideInInspector]
    public bool jump = false;

    public float jumpForce = 600;
    public GameObject basicAttack;

    private Transform groundCheck;
    private bool grounded = false;
    private Rigidbody2D body;

    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {
        groundCheck = transform.Find("groundCheck");
        body = GetComponent<Rigidbody2D>();
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
            body.velocity = new Vector2(Mathf.Sign(horInput) * maxSpeed, body.velocity.y);
        else
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }

        // Change facing direct
        if ((horInput > 0 && !facingRight) || (horInput < 0 && facingRight))
        {
            Flip();
        }

        // Jump
        if (jump)
        {
            body.AddForce(new Vector2(0, jumpForce));
            jump = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            BasicAttack();
        }
    }

    void BasicAttack()
    {
        Instantiate(basicAttack, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
}

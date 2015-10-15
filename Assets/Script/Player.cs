using UnityEngine;
using System.Collections;

public class Player : Character 
{
    protected bool jumped = false;
    public GameObject basicAttack;

    // Use this for initialization
    void Start()
    {

    }

    public override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Jump"))
            jumped = true;
    }

    void FixedUpdate()
    {
        float horInput = Input.GetAxisRaw("Horizontal");
        move(horInput);

        // Jump
        if (jumped)
        {
            jump();
            jumped = false;
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

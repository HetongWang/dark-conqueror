using UnityEngine;
using System.Collections.Generic;

public class Player : Character 
{
    protected bool jumped = false;
    protected bool dashed = false;

    public float dashSpeed = 2f;

    public GameObject basicAttack;

    protected Dictionary<string, int> buttonCount;
    protected Dictionary<string, float> buttonCooler;
    protected float buttonCoolerTime = 0.5f;

    // Use this for initialization
    void Start()
    {

    }

    public override void Awake()
    {
        base.Awake();
        buttonCooler = new Dictionary<string, float>();
        buttonCount = new Dictionary<string, int>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Jump"))
            jumped = true;
        Dash();
    }

    void FixedUpdate()
    {
        float horInput = Input.GetAxisRaw("Horizontal");
        // Detect if dash
        if (dashed)
            horInput *= dashSpeed;
        run(horInput);

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

    void Dash()
    {
        if (DoubleTapDetect("left") || DoubleTapDetect("right"))
            dashed = true;

        if (facingRight && Input.GetButtonUp("Horizontal"))
            dashed = false;
        if (!facingRight && Input.GetButtonUp("Horizontal"))
            dashed = false;
    }

    bool DoubleTapDetect(string key)
    {
        bool isKeyPress = false;
        bool res = false;

        if (key == "right")
        { 
            if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
                isKeyPress = true;
        }
        else if (key == "left")
        {
            if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
                isKeyPress = true;
        }  
        else if (Input.GetButtonDown(key))
        {
            isKeyPress = true;
        }

        if (!buttonCooler.ContainsKey(key))
        {
            buttonCooler.Add(key, 0);
            buttonCount.Add(key, 0);
        }

        List<string> keys = new List<string> (buttonCooler.Keys);
        foreach (string s in keys)
        {
            if (buttonCooler[s] > 0)
                buttonCooler[s] -= Time.deltaTime;
            if (buttonCooler[s] <= 0)
                buttonCount[s] = 0;
        }

        if (isKeyPress)
        {
            buttonCount[key] += 1;
            if (buttonCount[key] == 1)
            {
                buttonCooler[key] = buttonCoolerTime;
            }

            if (buttonCount[key] == 2 && buttonCooler[key] > 0)
            {
                buttonCount[key] = 0;
                res = true;
            }
        }

        return res;
    }
}

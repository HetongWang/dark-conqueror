using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {
    [HideInInspector]
    public bool facingRight = true;
    public float moveSpeed = 3f;

    public float jumpForce = 600;
    protected Transform groundCheck;
    protected bool grounded = false;
    protected bool movement = true;
    protected bool acting = false;

    public float hp = 10;
    protected Rigidbody2D body;

    public delegate IEnumerator skillFunction();
    protected Dictionary<string, skillFunction> skills = new Dictionary<string, skillFunction>();
    public Dictionary<string, float> skillCooler = new Dictionary<string, float>();

    /// <summary>
    /// Initialize method setting HP, AI, adding skills and other component
    /// </summary>
    public virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        groundCheck = transform.Find("groundCheck");
    }

    /// <summary>
    /// Automatically check hp and skill cd
    /// </summary>
    public virtual void Update()
    {
        detectGround();
        AliveOrDie();
        updateSkillCD();
    }

    private void updateSkillCD()
    {
        List<string> keys = new List<string> (skillCooler.Keys);
        foreach (string s in keys)
        {
            if (skillCooler[s] > 0)
                skillCooler[s] -= Time.deltaTime;
        }
    }

    protected void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x = scale.x * -1;
        transform.localScale = scale;
    }

    public void Hurt(float amount = 0)
    {
        hp -= amount;
    }

    public void AliveOrDie()
    {
        if (hp <= 0)
            Destroy(gameObject);
    }

    public void run(float horInput)
    {
        if (movement)
        {
            move(horInput);
        }
    }

    protected void move(float horInput)
    {
        // Control horizontal speed
        if (horInput != 0)
        {
            Vector2 v = body.velocity;
            if (Mathf.Abs(v.x) < moveSpeed * Mathf.Abs(horInput))
                v.x = moveSpeed * horInput;
            else if (Mathf.Sign(horInput) != Mathf.Sign(v.x))
                v.x += moveSpeed * horInput;
            body.velocity = v;
            //body.velocity = new Vector2(horInput * moveSpeed, body.velocity.y);
        }

        // Change facing direct
        if ((horInput > 0 && !facingRight) || (horInput < 0 && facingRight))
        {
            Flip();
        }
    }

    protected void move(float horInput, float verInput)
    {
        move(horInput);
        if (verInput != 0)
            body.velocity = new Vector2(body.velocity.x, verInput * moveSpeed);
        else
        {
            body.velocity = new Vector2(body.velocity.x, 0);
        }
    }

    protected void detectGround()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    public void jump()
    {
        if (grounded)
            body.AddForce(new Vector2(0, jumpForce));
    }

    /// <summary>
    /// Add skills. Use useSkill function to release skill
    /// </summary>
    /// <param name="name">skill name</param>
    /// <param name="f">skill IEnumerator function</param>
    /// <param name="cd"></param>
    protected void addSkill(string name, skillFunction f, float initCD = 0)
    {
        skills.Add(name, f);
        skillCooler.Add(name, initCD);
    }

    public void useSkill(string name, SkillSetting.skill setting, bool canMove = false, bool canBreak = false)
    {
        if (name != null && skillCooler[name] <= 0 && (!acting || canBreak))
        {
            movement = canMove;
            acting = true;
            StartCoroutine(skills[name]());
            skillCooler[name] = setting.cd;
            StartCoroutine(skillEnd(setting.duration));
        }
    }

    protected IEnumerator skillEnd(float duration)
    {
        yield return new WaitForSeconds(duration);
        movement = true;
        acting = false;
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract public class Character : MonoBehaviour
{
    public float hp;

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool died = false;
    public bool invincible = false;
    protected bool blocked = false;
    protected float dyingDuration = 1.1f;
    protected float hurtFlashTime = 0.3f;
    protected float hurtFlashAmount = 0.3f;
    protected Color hurtFlashColor = new Color(1, 1, 1);
    [HideInInspector]
    public float disappearTime = 4f;

    protected Transform groundCheck;
    protected bool grounded = false;
    protected float movementFreezenTime = 0;
    protected float actingTime = 0;
    protected float freezenTime = 0;
    protected float antiStaggerTime = 0;

    [HideInInspector]
    public Rigidbody2D body;
    protected Animator anim;
    private Shader defaultShader;
    public StatusEffectController statusController;

    public delegate IEnumerator skillFunction();
    protected Dictionary<string, skillFunction> skills = new Dictionary<string, skillFunction>();
    public Dictionary<string, float> skillCooler = new Dictionary<string, float>();
    protected Coroutine currentSkill = null;
    protected skillFunction interruptCallBack = null;
    [HideInInspector]
    public CharacterSet _setting;
    protected SkillSetting lastHurt;
    public int souls;

    /// <summary>
    /// Initialize method setting HP, AI, adding skills and other component
    /// </summary>
    public virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        groundCheck = transform.Find("groundCheck");
        statusController = new StatusEffectController(this);
    }

    public virtual void Start()
    {
        hp = _setting.hp;
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        defaultShader = r.material.shader;
        souls = _setting.souls;
    }

    /// <summary>
    /// Automatically check hp and skill cd
    /// </summary>
    public virtual void Update()
    {
        detectGround();
        updateStatus();
        AliveOrDie();
        updateSkillCD();
        statusController.updateStatus();
    }

    public void updateStatus()
    {
        movementFreezenTime = Mathf.Max(movementFreezenTime, freezenTime);
        if (movementFreezenTime > 0)
        {
            movementFreezenTime -= Time.deltaTime;
        }

        if (freezenTime > 0)
        {
            freezenTime -= Time.deltaTime;
            if (anim)
                anim.SetBool("hurt", true);
        }
        else
        {
            if (anim)
                anim.SetBool("hurt", false);
        }

        if (antiStaggerTime > 0)
        {
            antiStaggerTime -= Time.deltaTime;
        }

        if (actingTime > 0)
        {
            actingTime -= Time.deltaTime;
        }
        else
        {
            currentSkill = null;
            interruptCallBack = null;
        }

        if (anim)
            anim.SetFloat("moveSpeed", Mathf.Abs(body.velocity.x));
    }

    private void updateSkillCD()
    {
        List<string> keys = new List<string>(skillCooler.Keys);
        foreach (string s in keys)
        {
            if (skillCooler[s] > 0)
                skillCooler[s] -= Time.deltaTime;
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x = scale.x * -1;
        transform.localScale = scale;
    }

    protected bool canActing()
    {
        if (actingTime <= 0 && freezenTime <= 0)
            return true;
        else
            return false;
    }

    public virtual void Hurt(SkillSetting setting, Character source)
    {
        if (!invincible && !blocked)
        {
            getDemage(setting.damage);
            if (antiStaggerTime <= 0)
            {
                freezenTime = Mathf.Max(setting.freezenTime, freezenTime);
                cancelCurrentSkill();
                StartCoroutine(hurtFlash(hurtFlashColor));

                if (anim)
                {
                    anim.SetInteger("skill", 0);
                    anim.SetBool("hurt", true);
                }
            }
            else
            {
                StartCoroutine(GameManager.slowMotion(0.02f, 0.2f, 0f));
                StartCoroutine(hurtFlash(new Color(1, 0.4f, 0.4f)));
            }
            lastHurt = setting;
        }

        // Deal attack force
        if (setting.targetForce != null && antiStaggerTime <= 0)
        {
            Vector2 f = setting.targetForce;
            if (source != null)
            {
                if (source.facingRight)
                    f.x = Mathf.Abs(f.x);
                else
                    f.x = -Mathf.Abs(f.x);
            }
            body.AddForce(f);
        }
    }

    public virtual void getDemage(float amount)
    {
        if (!invincible)
        {
            hp -= amount;
            lastHurt = null;
        }
    }

    public void AliveOrDie()
    {
        if (died)
            return;
        if (hp <= 0)
        {
            died = true;
            actingTime = 9999999f;
            movementFreezenTime = 99999999f;
            anim.SetInteger("skill", 0);
            cancelCurrentSkill();
            StartCoroutine(dying());
        }

    }

    public virtual IEnumerator dying()
    {
        yield break;
    }

    public void run(float horInput)
    {
        if (movementFreezenTime <= 0)
        {
            move(horInput);
        }
    }

    protected void move(float horInput)
    {
        if (movementFreezenTime <= 0)
        {
            // Control horizontal speed
            if (horInput != 0)
            {
                Vector2 v = body.velocity;
                if (Mathf.Abs(v.x) < _setting.moveSpeed * Mathf.Abs(horInput))
                    v.x = _setting.moveSpeed * horInput;
                else if (Mathf.Sign(horInput) != Mathf.Sign(v.x))
                    v.x += _setting.moveSpeed * horInput;
                body.velocity = v;
                //body.velocity = new Vector2(horInput * moveSpeed, body.velocity.y);
            }

            // Change facing direct
            if ((horInput > 0 && !facingRight) || (horInput < 0 && facingRight))
            {
                Flip();
            }
        }
    }

    protected void moveVer(float verInput)
    {
        if (movementFreezenTime <= 0)
        {
            if (verInput != 0)
                body.velocity = new Vector2(body.velocity.x, verInput * _setting.moveSpeed);
            else
            {
                body.velocity = new Vector2(body.velocity.x, 0);
            }
        }
    }

    protected void move(float horInput, float verInput)
    {
        if (movementFreezenTime <= 0)
        {
            move(horInput);
            if (verInput != 0)
                body.velocity = new Vector2(body.velocity.x, verInput * _setting.moveSpeed);
            else
            {
                body.velocity = new Vector2(body.velocity.x, 0);
            }
        }
    }

    protected void detectGround()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (anim)
            anim.SetBool("grounded", grounded);
    }

    /// <summary>
    /// Add skills. Use useSkill function to release skill
    /// </summary>
    /// <param name="name">skill name</param>
    /// <param name="f">skill IEnumerator function</param>
    /// <param name="cd"></param>
    public void addSkill(string name, skillFunction f, float initCD = 0)
    {
        skills.Add(name, f);
        skillCooler.Add(name, initCD);
    }

    public void useSkill(string name, SkillSetting setting, skillFunction interruptCallBack, bool canMove = false, bool enforce = false)
    {
        useSkill(name, setting, canMove, enforce);
        this.interruptCallBack = interruptCallBack;
    }

    public void useSkill(string name, SkillSetting setting, float antiStaggerTime, bool canMove = false, bool enforce = false)
    {
        useSkill(name, setting, canMove, enforce);
        this.antiStaggerTime = antiStaggerTime;
    }
    public void useSkill(string name, SkillSetting setting, bool canMove = false, bool enforce = false)
    {
        if (name != null && skillCooler[name] <= 0 && (canActing() || enforce))
        {
            Debug.Log(name);
            if (enforce && currentSkill != null)
                cancelCurrentSkill();

            actingTime = setting.actDuration + 0.05f;
            currentSkill = StartCoroutine(skills[name]());
            skillCooler[name] = setting.cd;

            if (!canMove)
            {
                movementFreezenTime = setting.actDuration;
            }
        }
    }

    public void cancelCurrentSkill()
    {
        if (interruptCallBack != null)
            StartCoroutine(interruptCallBack());
        if (currentSkill != null)
            StopCoroutine(currentSkill);
    }

    public Vector3 childPosition(Vector2 offset)
    {
        Vector3 position = transform.position;
        if (facingRight)
        {
            position.x += Mathf.Abs(offset.x);
        }
        else
        {
            position.x -= Mathf.Abs(offset.x);
        }
        position.y += offset.y;
        return position;
    }

    public void addSelfForce(Vector2 force)
    {
        if (facingRight)
        {
            body.AddForce(force);
        }
        else
        {
            Vector2 f = new Vector2(-force.x, force.y);
            body.AddForce(f);
        }
    }

    public IEnumerator hurtFlash(Color c)
    {
        float timer = 0;
        float flashAmount = 0;
        float flashSpeed = hurtFlashAmount /(hurtFlashTime / 2);
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        r.material.shader = Shader.Find("Sprites/WhiteFlash");
        r.material.SetColor("_FlashColor", c);

        while (timer < 0.15f)
        {
            flashAmount += Time.deltaTime * flashSpeed;
            timer += Time.deltaTime;
            r.material.SetFloat("_FlashAmount", flashAmount);
            yield return new WaitForEndOfFrame();
        }

        while (timer < 0.3f)
        {
            flashAmount -= Time.deltaTime * flashSpeed;
            timer += Time.deltaTime;
            r.material.SetFloat("_FlashAmount", flashAmount);
            yield return new WaitForEndOfFrame();
        }

        r.material.shader = defaultShader;
        yield break;
    }

    public bool inAction()
    {
        return actingTime > 0 ? true : false;
    }
}

using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    public GameObject hpslider;

    [HideInInspector]
    public string behavior = null;
    protected BasicAI ai = null;
    protected GameObject hpBar;

    [HideInInspector]
    protected int souls = 5;

    public override void Awake()
    {
        base.Awake();
        hurtFlashColor = new Color(1, 0.4f, 0.4f);
    }

    public void setHPBar(Vector2 position, float initHP)
    {
        hpBar = (GameObject)Instantiate(hpslider, transform.position, Quaternion.Euler(0, 0, 0));
        HPBar slider = hpBar.GetComponent<HPBar>();
        hpBar.transform.parent = transform;
        slider.bind(this, initHP, position);
    }

    public override void Update()
    {
        base.Update();
        if (ai != null)
            behavior = ai.update();
    }

    public virtual void FixedUpdate()
    {
        if (behavior == "move")
        {
            run(ai.horMove());
        }
    }

    public override IEnumerator dying()
    {
        if (anim)
            anim.SetBool("dying", true);
        StartCoroutine(soulsExplosion());
        yield return new WaitForSeconds(dyingDuration);

        Destroy(hpBar);
        yield return new WaitForSeconds(disappearTime);
        Destroy(gameObject);
        yield break;
    }

    IEnumerator soulsExplosion()
    {
        Vector3 position = transform.position;
        position.y -= 0.2f;
        Object soulsExplosion = Instantiate(Resources.Load("SoulsExplosion"), position, Quaternion.Euler(0, 0, 0));
        for (int i = 0; i < souls; i++)
        {
            position = transform.position;
            position.x += Random.value - 0.5f;
            GameObject go = (GameObject)Instantiate(Resources.Load("Souls"), position, Quaternion.Euler(0, 0, 0));
            Souls s = go.GetComponent<Souls>();
            s.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, souls * 2 + 5));
            Debug.Log(lastHurt);
            if (lastHurt != null)
            {
                Debug.Log("lastHurt: " + lastHurt.GetType().Name);
                if (Mathf.Abs(lastHurt.targetForce.x) < 120f)
                    s.GetComponent<Rigidbody2D>().AddForce(new Vector2(30, 0));
                else
                    s.GetComponent<Rigidbody2D>().AddForce(lastHurt.targetForce / 3f);
            }
        }
        yield return new WaitForSeconds(0.1f);

        Destroy(soulsExplosion);
        yield break;
    }
}

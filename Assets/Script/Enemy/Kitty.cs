using UnityEngine;
using System.Collections;

public class Kitty: Character
{
    private KittyAI ai;

    public GameObject thrustAttackPrefab;
    protected Animator anim;

    public override void Awake()
    {
        base.Awake();
        addSkill("thrust", thrustAttack, KittySet.Instance.KittyThrust.cd);
        addSkill("enrage", enrage, KittySet.Instance.KittyEnrage.cd);
        facingRight = false;
        hp = 30;
        ai = new KittyAI(this);
        anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();
        useSkill(ai.attack(), KittySet.Instance.KittyThrust);
    }

    void FixedUpdate()
    {
        float horMove = ai.horMove();
        run(horMove);
    }

    public IEnumerator thrustAttack()
    {
        Vector3 position = transform.position;
        position.y += 0.2f;
        anim.SetInteger("attack", 1);

        if (facingRight) { 
            position.x += KittySet.Instance.KittyThrust.range / 2;
        }
        else
        {
            position.x -= KittySet.Instance.KittyThrust.range / 2;
        }

        GameObject gameo = (GameObject)Instantiate(thrustAttackPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        KittyThrustAttack thrust = gameo.GetComponent<KittyThrustAttack>();
        thrust.transform.parent = transform;
        thrust.setDriction(facingRight ? Vector2.right : Vector2.left);
        yield return new WaitForSeconds(KittySet.Instance.KittyThrust.duration);

        anim.SetInteger("attack", 0);
        yield break;
    }

    public IEnumerator enrage()
    {
        anim.SetBool("enrage", true);
        yield return new WaitForSeconds(KittySet.Instance.KittyEnrage.duration);

        anim.SetBool("enrage", false);
        yield break;
    }
}
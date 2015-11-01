using UnityEngine;
using System.Collections;

public class Kitty: Character
{
    private KittyAI ai;

    public GameObject thrustAttackPrefab;
    public GameObject enragePrefab;
    public GameObject kittyWolfPrefab;
    protected Animator anim;

    public bool enraged = false;

    public override void Awake()
    {
        base.Awake();
        addSkill("thrust", thrustAttack);
        addSkill("enrage", enrage);
        addSkill("summonWolf", summonWolf);

        facingRight = false;
        hp = KittySet.Instance.hp;

        ai = new KittyAI(this);
        anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();
        string attackName = ai.attack();
        switch (attackName)
        {
            case "enrage":
                useSkill(attackName, KittySet.Instance.KittyEnrage, false, true);
                break;
            default:
                useSkill(attackName, KittySet.Instance.KittyThrust);
                break;
        }
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
        thrust.init(facingRight ? Vector2.right : Vector2.left, enraged);

        yield return new WaitForSeconds(KittySet.Instance.KittyThrust.actDuration);

        anim.SetInteger("attack", 0);
        yield break;
    }

    public IEnumerator enrage()
    {
        enraged = true;
        Instantiate(enragePrefab, transform.position, Quaternion.Euler(Vector3.zero));
        anim.SetBool("enrage", true);
        yield return new WaitForSeconds(KittySet.Instance.KittyEnrage.actDuration);

        anim.SetBool("enrage", false);
        yield break;
    }

    public IEnumerator summonWolf()
    {
        anim.SetInteger("attack", 2);

        // First wolf
        Vector3 position = transform.position;
        position.y += 20;
        Instantiate(kittyWolfPrefab, position, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(KittySet.Instance.SummonWolf.actDuration);

        anim.SetInteger("attack", 0);
    }
}
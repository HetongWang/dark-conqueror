using UnityEngine;
using System.Collections;

public class Kitty: Enemy
{
    public GameObject thrustAttackPrefab;
    public GameObject enragePrefab;
    public GameObject kittyWolfPrefab;

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
        setHPBar(KittySet.Instance.hpBarOffset, KittySet.Instance.hp);
    }

    public override void Update()
    {
        base.Update();
        switch (behavior)
        {
            case "enrage":
                useSkill(behavior, KittySet.Instance.KittyEnrage, false, true);
                break;
            case "summonWolf":
                useSkill(behavior, KittySet.Instance.SummonWolf);
                break;
            default:
                useSkill(behavior, KittySet.Instance.KittyThrust);
                break;
        }
    }

    void FixedUpdate()
    {
        if (behavior == "move")
            run(ai.horMove());
    }

    public IEnumerator thrustAttack()
    {
        Vector3 position = transform.position;
        position.y += 0.2f;
        anim.SetInteger("attack", 1);

        if (facingRight)
        { 
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
        moveSpeed *= KittySet.Instance.enrageEnhancement;
        Instantiate(enragePrefab, transform.position, Quaternion.Euler(Vector3.zero));
        anim.SetBool("enrage", true);
        yield return new WaitForSeconds(KittySet.Instance.KittyEnrage.actDuration);

        anim.SetBool("enrage", false);
        Vector3 scale = transform.localScale;
        scale *= 1.1f;
        transform.localScale = scale;
        yield break;
    }

    public IEnumerator summonWolf()
    {
        anim.SetInteger("attack", 2);

        // First wolf
        Vector3 position = transform.position;
        position.y += 10;
        GameObject g1 = (GameObject)Instantiate(kittyWolfPrefab, position, Quaternion.Euler(0, 0, 0));
        KittyWolf w1 = g1.GetComponent<KittyWolf>();
        w1.setDirection(facingRight ? 1 : -1);

        // Second wolf
        position.x = ai.targetPlayer.transform.position.x;
        if (facingRight)
            position.x += 5f;
        else
            position.x -= 5f;
        GameObject g2 = (GameObject)Instantiate(kittyWolfPrefab, position, Quaternion.Euler(0, 0, 0));
        KittyWolf w2 = g2.GetComponent<KittyWolf>();
        w2.setDirection(!facingRight ? 1 : -1);

        yield return new WaitForSeconds(KittySet.Instance.SummonWolf.actDuration);

        anim.SetInteger("attack", 0);
        yield break;
    }
}
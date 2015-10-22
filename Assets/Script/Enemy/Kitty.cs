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
        addSkill("thrust", thrustAttack, KittyThrustAttack.CD);
        facingRight = false;
        hp = 30;
        ai = new KittyAI(this);
        anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();
        useSkill(ai.attack(), KittyThrustAttack.CD);
    }

    void FixedUpdate()
    {
        float horMove = ai.horMove(transform.position);
        run(horMove);
    }

    public IEnumerator thrustAttack()
    {
        Vector3 position = transform.position;
        anim.SetInteger("attack", 1);

        if (facingRight)
            position.x += KittyThrustAttack.Range / 2;
        else
            position.x -= KittyThrustAttack.Range / 2;
        Instantiate(thrustAttackPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        yield return new WaitForSeconds(KittyThrustAttack.Duration);

        anim.SetInteger("attack", 0);
        acting = false;
        yield break;
    }
}
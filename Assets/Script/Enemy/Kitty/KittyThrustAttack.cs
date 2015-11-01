using UnityEngine;
using System.Collections;

public class KittyThrustAttack : BasicAttack {

    public float forceIntensity = 50f;
    protected Vector2 force;

    public override void Awake()
    {
        setAttr(KittySet.Instance.KittyThrust);
        base.Awake();
        targetTag.Add("Player");
        setAnimator();
    }

    public override void getDemage(Collider2D col)
    {
        base.getDemage(col);
        StartCoroutine(attackPhase(col));
    }

    protected IEnumerator attackPhase(Collider2D col)
    {
        yield return new WaitForSeconds(duration / 3);
        col.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
        yield break;
    }

    public void init(Vector2 dirction, bool enraged)
    {
        if (enraged)
        {
            forceIntensity *= KittySet.Instance.enrageEnhancement;
            demage *= KittySet.Instance.enrageEnhancement;
        }
 
        force = dirction * forceIntensity;
        if (dirction.x > 0)
            anim.SetBool("faceRight", true);
        else
            anim.SetBool("faceRight", false);
    }
}

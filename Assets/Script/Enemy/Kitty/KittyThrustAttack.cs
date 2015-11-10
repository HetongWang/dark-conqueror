using UnityEngine;
using System.Collections;

public class KittyThrustAttack : BasicAttack {

    public float forceIntensity = KittySet.Instance.KittyThrustForceIntensity;
    protected Vector2 force;

    public override void Awake()
    {
        base.Awake();
        setAttr(KittySet.Instance.KittyThrust);
        targetTag.Add("Player");
        setAnimator();
        Destroy(gameObject, setting.actDuration);
    }

    public override void getDemage(Collider2D col)
    {
        base.getDemage(col);
        StartCoroutine(attackPhase(col));
    }

    protected IEnumerator attackPhase(Collider2D col)
    {
        yield return new WaitForSeconds(setting.actDuration / 4);
        col.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
        yield break;
    }

    public void init(Vector2 dirction, bool enraged)
    {
        if (enraged)
        {
            forceIntensity *= KittySet.Instance.enrageEnhancement;
            setting.demage *= KittySet.Instance.enrageEnhancement;
        }
 
        force = dirction * forceIntensity;
        if (dirction.x > 0)
            anim.SetBool("faceRight", true);
        else
            anim.SetBool("faceRight", false);
    }
}

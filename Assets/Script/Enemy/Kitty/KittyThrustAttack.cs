using UnityEngine;
using System.Collections;

public class KittyThrustAttack : BasicAttack {

    public override void Awake()
    {
        base.Awake();
        setAttr(KittySet.Instance.KittyThrust.clone());
        targetTag.Add("Player");
        Destroy(gameObject, setting.attackDuration);
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

    public void init(Character c, bool enraged)
    {
        owner = c;
        if (enraged)
        {
            force.x *= KittySet.Instance.enrageEnhancement;
            setting.damage *= KittySet.Instance.enrageEnhancement;
        }
    }
}

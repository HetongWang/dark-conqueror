using UnityEngine;
using System.Collections;

public class KittySlash : BasicAttack
{
    protected Vector2 force;

    public override void Awake()
    {
        base.Awake();
        setAttr(KittySet.Instance.Slash.clone());
        targetTag.Add("Player");
        Destroy(gameObject, setting.attackDuration);
    }

    public override void getDemage(Collider2D col)
    {
        base.getDemage(col);
        col.GetComponent<Rigidbody2D>().AddForce(force);
    }

    public void init(Vector2 dirction, bool enraged)
    {
        if (enraged)
        {
            setting.damage *= KittySet.Instance.enrageEnhancement;
        }

        force = dirction * KittySet.Instance.slashForce;
    }
}


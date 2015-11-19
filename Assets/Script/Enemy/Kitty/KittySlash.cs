using UnityEngine;
using System.Collections;

public class KittySlash : BasicAttack
{

    public override void Awake()
    {
        base.Awake();
        setAttr(KittySet.Instance.Slash.clone());
        targetTag.Add("Player");
        Destroy(gameObject, setting.attackDuration);
    }

    public void init(Character owner, bool enraged)
    {
        this.owner = owner;
        if (enraged)
        {
            setting.damage *= KittySet.Instance.enrageEnhancement;
        }
    }
}


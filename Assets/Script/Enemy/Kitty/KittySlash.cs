using UnityEngine;
using System.Collections;

public class KittySlash : BasicAttack
{
    public override void Awake()
    {
        base.Awake();
        targetTag.Add("Player");
    }

    void Start()
    {
        Destroy(gameObject, setting.attackDuration);
    }
}


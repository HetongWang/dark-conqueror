using UnityEngine;
using System.Collections;

public class KittySlash : BasicAttack
{
    private Kitty owner;

    public override void Awake()
    {
        base.Awake();
        targetTag.Add("Player");
    }

    void Start()
    {
        owner = (Kitty)_owner;
        Destroy(gameObject, setting.attackDuration);
    }
}


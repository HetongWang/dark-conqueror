using UnityEngine;

public class KittyWolfAttack : BasicAttack
{
    public override void Awake()
    {
        base.Awake();
        targetTag.Add("Player");
    }
}

using UnityEngine;

public class KittyWolfAttack : BasicAttack
{
    public override void Awake()
    {
        base.Awake();
        setting = KittySet.SummonWolf;
        targetTag.Add("Player");
    }
}

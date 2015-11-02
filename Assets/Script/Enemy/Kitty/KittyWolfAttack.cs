using UnityEngine;

public class KittyWolfAttack : BasicAttack
{
    public override void Awake()
    {
        base.Awake();
        demage = KittySet.Instance.SummonWolf.demage;
        targetTag.Add("Player");
    }
}

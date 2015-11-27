using UnityEngine;
using System.Collections;

public class KittyEnrage : BasicAttack
{
    private Kitty owner;

    void Start()
    {
        owner = (Kitty)_owner;
        GetComponent<PointEffector2D>().forceMagnitude = owner.setting.KittyEnrage.targetForce.x;
        Destroy(gameObject, owner.setting.KittyEnrage.attackDuration);
    }
}

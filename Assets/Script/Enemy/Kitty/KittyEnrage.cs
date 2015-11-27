using UnityEngine;
using System.Collections;

public class KittyEnrage : MonoBehaviour
{
    void Awake()
    {
        GetComponent<PointEffector2D>().forceMagnitude = KittySet.KittyEnrage.targetForce.x;
        Destroy(gameObject, KittySet.KittyEnrage.attackDuration);
    }
}

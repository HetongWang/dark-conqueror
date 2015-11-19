using UnityEngine;
using System.Collections;

public class KittyEnrage : MonoBehaviour
{
    void Awake()
    {
        GetComponent<PointEffector2D>().forceMagnitude = KittySet.Instance.KittyEnrage.targetForce.x;
        Destroy(gameObject, KittySet.Instance.KittyEnrage.attackDuration);
    }
}

using UnityEngine;
using System.Collections;

public class KittyEnrage : MonoBehaviour
{
    void Awake()
    {
        Destroy(gameObject, KittySet.Instance.KittyEnrage.attackDuration);
    }
}

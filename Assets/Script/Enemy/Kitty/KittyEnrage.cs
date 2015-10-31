using UnityEngine;
using System.Collections;

public class KittyEnrage : MonoBehaviour
{
    void Awake()
    {
        Destroy(gameObject, 0.1f);
    }
}

using UnityEngine;
using System.Collections;

public class TwoCatwolfFall : MonoBehaviour
{
    public GameObject catwolfPrefab;
    public bool triggered = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (triggered)
            return;

        float cameraBorder;
        cameraBorder = Camera.main.transform.position.x;
        cameraBorder -= 2f;
        Vector3 position = new Vector3(cameraBorder, transform.position.y, transform.position.z);
        position.y += 10f;
        Instantiate(catwolfPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));

        cameraBorder += 5f;
        position = new Vector3(cameraBorder, transform.position.y, transform.position.z);
        Instantiate(catwolfPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        triggered = true; 
    }
}

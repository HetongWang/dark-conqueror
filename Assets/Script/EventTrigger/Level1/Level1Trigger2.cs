using UnityEngine;
using System.Collections;

public class Level1Trigger2 : MonoBehaviour
{
    public GameObject rotopollyPrefab;
    public bool triggered = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (triggered)
            return;

        float cameraBorder;
        cameraBorder = Camera.main.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height;
        cameraBorder -= 2f;

        Vector3 position = new Vector3(cameraBorder, transform.position.y, transform.position.z);
        position.y += Camera.main.transform.position.x + Camera.main.orthographicSize;
        Instantiate(rotopollyPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));

        position.x -= 2f;
        Instantiate(rotopollyPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));

        position.x -= 2f;
        Instantiate(rotopollyPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        triggered = true; 
    }
}

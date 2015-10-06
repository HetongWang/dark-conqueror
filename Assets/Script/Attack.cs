using UnityEngine;
using System.Collections.Generic;

public class Attack : MonoBehaviour {

    private List<GameObject> hurted;

    void Awake()
    {
        hurted = new List<GameObject>();
        Destroy(gameObject, 0.2f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("attack");
        if (col.tag == "Enemy")
        {
            foreach (GameObject i in hurted)
            {
                if (i == col.gameObject)
                {
                    return;
                }
            }

            col.gameObject.GetComponent<MooControl>().Hurt();
            hurted.Add(col.gameObject);
        }
    } 
}

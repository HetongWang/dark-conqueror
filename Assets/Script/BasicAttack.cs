using UnityEngine;
using System.Collections.Generic;

public class BasicAttack : MonoBehaviour {

    private List<GameObject> hurted;
    public string targetTag = "Enemy";

    void Awake()
    {
        hurted = new List<GameObject>();
        Destroy(gameObject, 0.2f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == targetTag)
        {
            foreach (GameObject i in hurted)
            {
                if (i == col.gameObject)
                {
                    return;
                }
            }

            col.gameObject.GetComponent<Character>().Hurt();
            hurted.Add(col.gameObject);
        }
    } 
}

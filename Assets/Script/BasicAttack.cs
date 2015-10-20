using UnityEngine;
using System.Collections.Generic;

public class BasicAttack : MonoBehaviour {

    private List<GameObject> hurted;
    public string targetTag = "Enemy";
    public float demage = 1;
    static public float duration = 0.6f;
    static public float cd = 2;

    public virtual void Awake()
    {
        hurted = new List<GameObject>();
        Destroy(gameObject, duration);
    }

    public virtual void OnTriggerEnter2D(Collider2D col)
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

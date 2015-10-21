using UnityEngine;
using System.Collections.Generic;

public class BasicAttack : MonoBehaviour {

    private List<GameObject> hurted;
    public string targetTag = "Enemy";
    protected float _demage = 1;
    protected float _duration = 0.5f;
    protected float _cd = 2;

    protected Animator anim;

    public virtual void Awake()
    {
        hurted = new List<GameObject>();
        Destroy(gameObject, _duration);
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

            col.gameObject.GetComponent<Character>().Hurt(_demage);
            hurted.Add(col.gameObject);
        }
    } 

    protected void setAnimator(Animator anim)
    {
        this.anim = anim;
    }
}

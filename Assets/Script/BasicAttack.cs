using UnityEngine;
using System.Collections.Generic;

public class BasicAttack : MonoBehaviour {

    private List<GameObject> hurted;
    public string targetTag = "Enemy";
    protected float demage;
    protected float duration;
    protected float cd;

    protected Animator anim;

    /// <summary>
    /// Awake function. involke setAttr before base.Awake()
    /// </summary>
    public virtual void Awake()
    {
        hurted = new List<GameObject>();
        Destroy(gameObject, duration);
    }

    protected void OnTriggerEnter2D(Collider2D col)
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

            getDemage(col);
        }
    } 

    public virtual void getDemage(Collider2D col)
    {
        col.gameObject.GetComponent<Character>().Hurt(demage);
        hurted.Add(col.gameObject);
    }

    protected void setAnimator()
    {
        anim = GetComponent<Animator>();
    }
    
    public void setAttr(SkillSetting.skill skill)
    {
        duration = skill.duration;
        cd = skill. cd;
        demage = skill.demage;
    }
}

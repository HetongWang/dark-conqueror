using UnityEngine;
using System.Collections.Generic;

public class BasicAttack : MonoBehaviour {

    private List<GameObject> hurted;
    public List<string> targetTag = new List<string>();
    protected SkillSetting setting;
    [HideInInspector]
    public Character owner;

    protected Animator anim;

    /// <summary>
    /// Awake function.
    /// </summary>
    public virtual void Awake()
    {
        hurted = new List<GameObject>();
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (targetTag.Contains(col.tag))
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
        Character person = col.gameObject.GetComponent<Character>();
        if (person)
        {
            person.Hurt(setting, owner);

            hurted.Add(col.gameObject);
        }
    }

    protected void setAnimator()
    {
        anim = GetComponent<Animator>();
    }

    public void setAttr(SkillSetting setting)
    {
        this.setting = setting;
    }
    
    public virtual void init(Character c, SkillSetting setting)
    {
        owner = c;
        this.setting = setting;
    }
}

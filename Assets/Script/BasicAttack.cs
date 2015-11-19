using UnityEngine;
using System.Collections.Generic;

public class BasicAttack : MonoBehaviour {

    private List<GameObject> hurted;
    public List<string> targetTag = new List<string>();
    protected SkillSetting setting;
    public Vector2 force;
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
            person.Hurt(setting);

            Vector2 f = setting.targetForce;

            if (owner != null)
            {
                if (owner.facingRight)
                    f.x = Mathf.Abs(f.x);
                else
                    f.x = -Mathf.Abs(f.x);
            }
            person.body.AddForce(f);

            hurted.Add(col.gameObject);
        }
    }

    protected void setAnimator()
    {
        anim = GetComponent<Animator>();
    }
    
    public void setAttr(SkillSetting skill)
    {
        setting = skill;
    }
    
    public virtual void init(Character c)
    {
        owner = c;
    }
}

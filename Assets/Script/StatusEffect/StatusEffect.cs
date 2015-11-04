using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatusEffect
{
    public GameObject prefab;

    protected Character person;
    public float duration;
    public int maxOverlay;
    public int overlayCount = 0;
    protected List<float> timer = new List<float>();

    public StatusEffect(Character person)
    {
        this.person = person;
    }

    public virtual void Update()
    {
        for (int i = 0; i < timer.Count; i++)
        {
            timer[i] -= Time.deltaTime;
            if (timer[i] < 0)
                timer.RemoveAt(i);
        }
        overlayCount = timer.Count;

        for (int i = 0; i < timer.Count; i++)
        {
            effect();           
        }
    }

    public void reset()
    {
        timer.RemoveAt(0);
        addNew();
    }

    public void addNew()
    {
        if (overlayCount < maxOverlay)
        {
            timer.Add(duration);
        }
    }

    public virtual void effect()
    {

    }

    ~StatusEffect()
    {
        Object.Destroy(prefab);
    }
}

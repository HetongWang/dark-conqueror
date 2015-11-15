using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatusEffectController
{
    public Dictionary<string, StatusEffect> status = new Dictionary<string, StatusEffect>();

    protected Character person;

    public StatusEffectController(Character person)
    {
        this.person = person;
    }

    public void addStatus(StatusEffect effect)
    {
        string name = effect.GetType().Name;
        if (!status.ContainsKey(name))
        {
            StatusEffect instance = effect;
            if (instance != null)
            {
                status.Add(name, instance);
                status[name].addNew();
            }
            else
                Debug.Log("No status effect named " + name);
        }
        else
        {
            if (status[name].overlayCount < status[name].maxOverlay)
                status[name].addNew();
            else
                status[name].reset();
        }
    }

    public void updateStatus()
    {
        List<string> keys = new List<string> (status.Keys);
        foreach (string s in keys)
        {
            status[s].Update();
            if  (status[s].overlayCount <= 0)
            {
                status[s] = null;
                status.Remove(s);
            }
        }
    }
}

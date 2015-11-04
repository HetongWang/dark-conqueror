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

    public void addStatus(string name)
    {
        if (!status.ContainsKey(name))
        {
            StatusEffect instance = newStatusInstance(name);
            if (instance != null)
                status.Add(name, instance);
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

    public StatusEffect newStatusInstance(string name)
    {
        StatusEffect res = null;
        switch (name)
        {
            case "burn":
                res = new BurnStatus(person);
                break;
        }

        return res;
    }
}

using UnityEngine;
using System.Collections.Generic;

public class Level1Trigger0 : MonoBehaviour
{
    public bool triggered = false;
    protected List<Dialog> conver;

    void Start()
    {
        conver = new List<Dialog>();
        conver.Add(new Dialog("PC", "This is the heart of the Weald. Raegor should be near.  "));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (triggered)
            return;

        GameManager.Instance.newConversation(conver);
        triggered = true; 
    }
}

using UnityEngine;
using System.Collections.Generic;

public class Level1Trigger0 : MonoBehaviour
{
    public bool triggered = false;
    protected List<Dialog> conver;

    void Start()
    {
        conver = new List<Dialog>();
        conver.Add(new Dialog("PC", "For years I have ruled over the human kingdoms... "));
        conver.Add(new Dialog("PC", "But the savage farlands still remain beyond my grasp. Raegor the Shadowstalker, Highseer Pakki, Agamond the Watcher--these are the names of the rulers who must die before Azrelia can be unified."));
        conver.Add(new Dialog("PC", "I go forth to end their reign. By my lonesome, as I have ever."));
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

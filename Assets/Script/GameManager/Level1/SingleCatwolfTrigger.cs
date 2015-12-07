using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SingleCatwolfTrigger : MonoBehaviour
{
    public GameObject catwolfPrefab;
    public bool triggered = false;
    protected List<Dialog> conver;

    IEnumerator con()
    {
        yield return new WaitForSeconds(1f);
        conver = new List<Dialog>();
        conver.Add(new Dialog("PC", "'J' to normal attack. 'K' to heavy attack. 'L' use magic."));
        conver.Add(new Dialog("PC", "Magic can be gathered from enemies when you attack him."));
        conver.Add(new Dialog("PC", "'H' to block. double tap movenment key to dash."));
        conver.Add(new Dialog("PC", "When you dash, you are invincible. However dash and block will cost you stamina"));
        conver.Add(new Dialog("PC", "When you kill an enemy, you can gather their souls to upgrade your skills. It also will recover your health"));
        GameManager.Instance.newConversation(conver);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (triggered)
            return;

        StartCoroutine(con());
        float cameraBorder;
        cameraBorder = Camera.main.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height;
        cameraBorder -= 2f;
        Vector3 position = new Vector3(cameraBorder, transform.position.y, transform.position.z);
        Instantiate(catwolfPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        triggered = true; 
    }
}

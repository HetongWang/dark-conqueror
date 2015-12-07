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
        conver.Add(new Dialog("PC", "[ A ] [ D ] to move left or right, double tapping to dash. [ W ] or [ Spacebar ] to jump."));
        conver.Add(new Dialog("PC", "[ J ] for basic attack. [ K ] for heavy attack. [ Jump + K ] to perform a heavy downward attack."));
        conver.Add(new Dialog("PC", "Hold [ H ] to block. Both block and dash cost stamina."));
        conver.Add(new Dialog("PC", "[ L ] to cast Eruption. Magic is a limited resource that you can refill by striking enemies with normal attacks."));
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

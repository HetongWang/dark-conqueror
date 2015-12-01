using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level1Trigger5 : MonoBehaviour
{
    public GameObject kittyPrefab;
    public bool triggered = false;
    protected List<Dialog> conver;

    void Start()
    {
        conver = new List<Dialog>();
        conver.Add(new Dialog("Kitty", "Brazen trespasser... Think you the hunter? You will find only death here."));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (triggered)
            return;

        float cameraBorder;
        cameraBorder = Camera.main.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height;
        cameraBorder += 3f;

        Vector3 position = new Vector3(cameraBorder, transform.position.y + 10f, transform.position.z);
        Instantiate(kittyPrefab, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        StartCoroutine(delay());
        triggered = true;
    }

    protected IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.newConversation(conver);
    }
}

using UnityEngine;
using System.Collections;

public class DeathUI : MonoBehaviour
{
    protected Character person;
    public void Start()
    {
        person = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
    }

    void Update()
    {
        if (person.died)
            GetComponent<Canvas>().enabled = true;
    }
}

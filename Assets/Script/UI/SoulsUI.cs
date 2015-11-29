using UnityEngine;
using System.Collections;

public class SoulsUI : MonoBehaviour
{
    protected Player person;
    UnityEngine.UI.Text text;

    public virtual void Awake()
    {
        person = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        text = GetComponent<UnityEngine.UI.Text>();
    }

    void Update()
    {
        text.text = "Souls: " + person.souls;
    }
}

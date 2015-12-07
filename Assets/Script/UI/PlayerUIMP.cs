using UnityEngine;
using System.Collections;

public class PlayerUIMP: MonoBehaviour
{
    protected Player person;
    public float value;
    public float initValue;
    public float velocity;

    UnityEngine.UI.Image bar;

    public virtual void Awake()
    {
        bar = GetComponent<UnityEngine.UI.Image>();
    }

    public void Start()
    {
        person = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        initValue = person.setting.magic;
    }

    void Update()
    {
        value = person.magic;
        bar.fillAmount = Mathf.SmoothDamp(bar.fillAmount, value / initValue, ref velocity, 0.2f);
    }

}

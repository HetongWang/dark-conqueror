using UnityEngine;
using System.Collections;

public class PlayerUIStamina: MonoBehaviour
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
        initValue = person.setting.stamina;
    }

    void Update()
    {
        value = person.stamina;
        bar.fillAmount = value / initValue;
        bar.fillAmount = Mathf.SmoothDamp(bar.fillAmount, value / initValue, ref velocity, 0.2f);
    }

}

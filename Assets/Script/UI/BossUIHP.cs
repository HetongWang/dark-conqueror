using UnityEngine;
using System.Collections;

public class BossUIHP: MonoBehaviour
{
    protected Character person;
    public float value;
    public float initValue;
    public float velocity;

    UnityEngine.UI.Image bar;

    public virtual void Awake()
    {
        bar = GetComponent<UnityEngine.UI.Image>();
    }

    public void init(Character boss)
    {
        person = boss;
        initValue = person._setting.hp;
    }

    void Update()
    {
        value = person.hp;
        bar.fillAmount = Mathf.SmoothDamp(bar.fillAmount, value / initValue, ref velocity, 0.2f);
    }

}

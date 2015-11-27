using UnityEngine;
using System.Collections;

public class PlayerUIHPSlider : MonoBehaviour
{
    protected Character person;
    public float value;
    public float initValue;

    UnityEngine.UI.Image bar;
    private Vector3 barScale;

    public virtual void Awake()
    {
        person = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        initValue = PlayerSet.hp;
        bar = GetComponent<UnityEngine.UI.Image>();
        barScale = transform.localScale;
    }

    void Update()
    {
        value = person.hp;
        // Set the health bar's colour to proportion of the way between green and red based on the player's health.
        bar.color = Color.Lerp(Color.green, Color.red, 1 - value / initValue);

        // Set the scale of the value bar to be proportional to the player's value.
        transform.localScale = new Vector3(barScale.x * value / initValue, 1, 1);
    }

    public void bind(Character ch, float initHP)
    {
        person = ch;
        initValue = initHP;
    }
}

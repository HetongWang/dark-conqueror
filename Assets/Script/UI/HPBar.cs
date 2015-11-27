using UnityEngine;
using System.Collections;

public class HPBar : MonoBehaviour
{
    protected Character person;
    public float value;
    public float initValue;

    Transform bar;
    private Vector3 barScale;
    protected Vector2 offset;

    public virtual void Awake()
    {
        bar = transform.FindChild("HPBar");
        barScale = transform.localScale;
    }

    void Update()
    {
        if (person == null)
        {
            Destroy(gameObject);
            return;
        }

        value = person._setting.hp;
        if (value < 0)
            value = 0;
        // Set the scale of the value bar to be proportional to the player's value.
        bar.localScale = new Vector3(barScale.x * value / initValue, 1, 1);
    }

    public void bind(Character ch, float initHP, Vector2 offset)
    {
        person = ch;
        initValue = initHP;
        Vector3 v = new Vector3(offset.x, offset.y, 0);
        v.x += transform.localPosition.x;
        v.y += transform.localPosition.y;
        transform.localPosition = v;
    }
}

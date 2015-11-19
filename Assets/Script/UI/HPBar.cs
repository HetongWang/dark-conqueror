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

        value = person.hp;
        // Set the scale of the value bar to be proportional to the player's value.
        bar.localScale = new Vector3(barScale.x * value / initValue, 1, 1);
        Vector3 position = new Vector3(person.transform.position.x + offset.x, person.transform.position.y + offset.y, 0);
        transform.position = position;
    }

    public void bind(Character ch, float initHP, Vector2 offset)
    {
        person = ch;
        initValue = initHP;
        this.offset = offset;
    }
}

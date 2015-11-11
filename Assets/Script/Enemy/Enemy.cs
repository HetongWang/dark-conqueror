using UnityEngine;

public class Enemy : Character
{
    public GameObject hpslider;
    public string behavior = null;

    protected BasicAI ai = null;

    public void setHPBar(Vector2 position, float initHP)
    {
        GameObject go = (GameObject)Instantiate(hpslider, transform.position, Quaternion.Euler(0, 0, 0));
        HPBar slider = go.GetComponent<HPBar>();
        slider.bind(this, initHP, position);
    }

    public override void Update()
    {
        base.Update();
        if (ai != null)
            behavior = ai.update();
    }

    public virtual void FixedUpdate()
    {
        if (behavior == "move")
        {
            Debug.Log("move");
            run(ai.horMove());
        }
    }
}

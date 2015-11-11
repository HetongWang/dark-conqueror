using UnityEngine;

public class Enemy : Character
{
    public GameObject hpslider;
    public string behavior = null;

    protected BasicAI ai;

    public void setHPBar(Vector2 position, float initHP)
    {
        GameObject go = (GameObject)Instantiate(hpslider, transform.position, Quaternion.Euler(0, 0, 0));
        HPBar slider = go.GetComponent<HPBar>();
        slider.bind(this, initHP, position);
    }

    public override void Update()
    {
        base.Update();
        behavior = ai.update();
    }

    public virtual void FixedUpdate()
    {
        if (behavior == "move")
            run(ai.horMove());
    }
}

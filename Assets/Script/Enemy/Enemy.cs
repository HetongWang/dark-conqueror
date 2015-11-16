using UnityEngine;

public class Enemy : Character
{
    public GameObject hpslider;

    [HideInInspector]
    public string behavior = null;
    protected BasicAI ai = null;

    [HideInInspector]
    public int souls;

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
        else
            Debug.Log("Lack of AI");
    }

    public virtual void FixedUpdate()
    {
        if (behavior == "move")
        {
            run(ai.horMove());
        }
    }

    void OnDestroy()
    {
        Debug.Log(this.GetType().Name);
        Player player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        player.souls += souls;
    }
}

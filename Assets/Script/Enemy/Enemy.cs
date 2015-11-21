using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    public GameObject hpslider;

    [HideInInspector]
    public string behavior = null;
    protected BasicAI ai = null;
    protected GameObject hpBar;

    [HideInInspector]
    public int souls;

    public void setHPBar(Vector2 position, float initHP)
    {
        hpBar = (GameObject)Instantiate(hpslider, transform.position, Quaternion.Euler(0, 0, 0));
        HPBar slider = hpBar.GetComponent<HPBar>();
        hpBar.transform.parent = transform;
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
            run(ai.horMove());
        }
    }

    public override IEnumerator dying()
    {
        if (anim)
            anim.SetBool("dying", true);
        yield return new WaitForSeconds(dyingDuration);

        Destroy(hpBar);
        yield return new WaitForSeconds(disappearTime);
        Destroy(gameObject);
        yield break;
    }

    void OnDestroy()
    {
        Player player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        player.souls += souls;
    }
}

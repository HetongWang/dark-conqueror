using UnityEngine;

public class Enemy : Character
{
    public GameObject hpslider;

    public void setHPBar(Vector2 position, float initHP)
    {
        GameObject go = (GameObject)Instantiate(hpslider, transform.position, Quaternion.Euler(0, 0, 0));
        HPBar slider = go.GetComponent<HPBar>();
        slider.bind(this, initHP, position);
    }
}

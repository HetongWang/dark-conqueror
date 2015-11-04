using UnityEngine;
using System.Collections;

public class PlayerHPSlider : UIHPSlider {

    public override void Awake()
    {
        base.Awake();
        person = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        initValue = PlayerSet.Instance.hp;
    }
}

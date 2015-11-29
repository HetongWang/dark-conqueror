﻿using UnityEngine;
using System.Collections.Generic;

public class SoulsCollector : MonoBehaviour
{
    public Player player;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Souls s = col.GetComponent<Souls>();
        s.disappear();
        player.souls += 1;
    }
}
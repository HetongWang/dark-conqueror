using UnityEngine;
using System.Collections.Generic;

public class UpgradeMenuCanvas : MonoBehaviour
{
    public Player pc;

    void OnEnable()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        Time.timeScale = 1;
    }
}

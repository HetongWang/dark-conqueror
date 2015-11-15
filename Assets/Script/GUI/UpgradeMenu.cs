using UnityEngine;
using System.Collections.Generic;

public class UpgradeMenu : MonoBehaviour
{
    public Player pc;

    void Awake()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Time.timeScale = 0;
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 4, Screen.height / 4));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();

        GUILayout.Label("3rd normal attack with burn\n Spend 1 souls");
        if (GUILayout.Button("upgrade to " + (pc.normalAttackLevel + 1)))
        {
            if (pc.souls >= 1)
            {
                pc.normalAttackLevel += 1;
                Debug.Log(pc.normalAttackLevel);
                pc.souls -= 1;
            }
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

    }

    void OnDestroy()
    {
        Time.timeScale = 1;
    }
}

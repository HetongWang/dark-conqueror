using UnityEngine;
using System.Collections.Generic;

public class UpgradeMenu : MonoBehaviour
{
    public Player pc;
    public int normalAttack = 3;
    public int eruptionFire= 3;

    void Awake()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Time.timeScale = 0;
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();

        GUILayout.Label("3rd normal attack with burn. Spend " + normalAttack + " souls");
        if (GUILayout.Button("upgrade to " + (pc.normalAttackLevel + 1)))
        {
            if (pc.souls >= normalAttack)
            {
                pc.normalAttackLevel += 1;
                pc.souls -= normalAttack;
            }
        }

        GUILayout.Label("Eruption Fire: Increas one eruption" + eruptionFire + " souls");
        if (GUILayout.Button("upgrade to " + (pc.setting.eruptionFireTimes + 1)))
        {
            if (pc.souls >= eruptionFire)
            {
                pc.setting.eruptionFireTimes += 1;
                pc.setting.eruptionFire.actDuration += 0.2f / (pc.setting.eruptionFireTimes - 2);
                pc.souls -= eruptionFire;
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

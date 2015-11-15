using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject UpgradeMenu;
    protected GameObject menu;

    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (menu == null)
                menu = (GameObject)Instantiate(UpgradeMenu, Vector3.zero, Quaternion.Euler(0, 0, 0));
            else
                Destroy(menu);
        }
    }
}

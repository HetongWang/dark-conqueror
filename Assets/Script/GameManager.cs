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

    public static IEnumerator slowMotion(float scale, float freezenTime, float time)
    {
        float timer = Time.time;
        Time.timeScale = scale;
        yield return new WaitForSeconds(freezenTime);

        while (true)
        {
            Time.timeScale = Mathf.Lerp(scale, 1, timer / time);
            if (Time.time - timer > time)
                break;
            yield return new WaitForEndOfFrame();
        }
        Time.timeScale = 1;
        yield break;
    }
}

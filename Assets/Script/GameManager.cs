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
        float timer = 0;
        Time.timeScale = scale;
        yield return new WaitForSeconds(freezenTime * Time.timeScale);

        while (true && time > 0)
        {
            Time.timeScale = Mathf.Lerp(scale, 1, timer / time);
            timer += Time.deltaTime / Time.timeScale;
            if (timer >= time)
                break;
            yield return new WaitForEndOfFrame();
        }
        Time.timeScale = 1;
        yield break;
    }
}

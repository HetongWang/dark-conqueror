using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    public GameObject UpgradeMenuPrefab;
    public GameObject HUDPrefab;
    protected GameObject menu;
    protected GameObject hud;
    protected ConversationManager cm;
    public bool inConversation = false;

    void Start()
    {
        hud = Instantiate(HUDPrefab);
        cm = new ConversationManager(hud);
    }

    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (menu == null)
                menu = (GameObject)Instantiate(UpgradeMenuPrefab, Vector3.zero, Quaternion.Euler(0, 0, 0));
            else
                Destroy(menu);
        }
        if (inConversation)
        {
            if (Input.GetButtonDown("Submit") || 
                Input.GetButtonDown("NormalAttack") ||
                Input.GetButtonDown("Jump"))
            {
                cm.nextDialog();
            }
        }
    }

    public void newConversation(List<Dialog> con)
    {
        inConversation = true;
        Time.timeScale = 0;
        cm.newConversation(con);
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

﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    public GameObject UpgradeMenuPrefab;
    public GameObject HUDPrefab;
    public Sprite PCPicture;
    public Sprite KittyPicture;
    protected ConversationManager cm;
    protected GameObject hud;
	protected GameObject upgradeMenu;
	protected GameObject endMenu;
	protected bool paused;
    public bool inConversation = false;


    void Start()
    {
        hud = Instantiate(HUDPrefab);
        upgradeMenu = Instantiate(UpgradeMenuPrefab);
        upgradeMenu.SetActive(false);
        cm = new ConversationManager(hud);
        cm.PCPicture = PCPicture;
        cm.KittyPicture = KittyPicture;
    }

    void Update()
    {
		if (Input.GetButtonDown("Menu"))
		{
			if (!paused)
            {
				upgradeMenu.SetActive(true);
				hud.SetActive(false);
				paused = true;
			} 
            else
            {
				upgradeMenu.SetActive(false);
				hud.SetActive(true);
				paused = false;
			}
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
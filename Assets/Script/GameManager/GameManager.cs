using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    public GameObject UpgradeMenuPrefab;
    public GameObject HUDPrefab;
    public Sprite PCPicture;
    public Sprite KittyPicture;
    protected GameObject menu;
    protected GameObject hud;
    protected ConversationManager cm;
	protected GameObject pauseMenu;
	protected GameObject mainUI;
	protected GameObject endMenu;
	protected bool paused;
    public bool inConversation = false;


    void Start()
    {
        hud = Instantiate(HUDPrefab);
        cm = new ConversationManager(hud);
        cm.PCPicture = PCPicture;
        cm.KittyPicture = KittyPicture;

		pauseMenu = GameObject.FindGameObjectsWithTag("PauseMenu")[0];
		pauseMenu.GetComponent<UnityEngine.Canvas>().enabled = true;
		pauseMenu.SetActive(false);
		mainUI = GameObject.FindGameObjectsWithTag("UI")[0];
    }

    void Update()
    {
		if (Input.GetButtonDown("Menu"))
		{
			// if (menu == null)
			if (paused == false){
				pauseMenu.SetActive(true);
				mainUI.SetActive(false);
				paused = true;
				//menu = (GameObject)Instantiate(UpgradeMenu, Vector3.zero, Quaternion.Euler(0, 0, 0));
				
			} else {
				pauseMenu.SetActive(false);
				mainUI.SetActive(true);
				paused = false;
				//Destroy(menu);
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

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject UpgradeMenu;
    protected GameObject menu;
	private bool paused;
	protected GameObject pauseMenu;
	public GameObject mainUI;

	void Awake(){
		pauseMenu = GameObject.FindGameObjectsWithTag("PauseMenu")[0];
		pauseMenu.GetComponent<UnityEngine.Canvas>().enabled = true;
		pauseMenu.SetActive(false);

		mainUI = GameObject.FindGameObjectsWithTag("UI")[0];
	}

    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
//            if (menu == null)
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

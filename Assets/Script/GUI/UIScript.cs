using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {
	public GameObject currentMenu;
	public GameObject hud;

	public virtual void Awake(){
		currentMenu = GameObject.FindGameObjectsWithTag("PauseMenu")[0];
		//hud = GameObject.FindGameObjectsWithTag("UI")[1];
	}

	public void StartGame(){
		Application.LoadLevel(1);

	}

	public void RestartGame(){
		Application.LoadLevel(1);
	}

	public void SkillUp(){
		//increase skill here
		//based on soul count
		//change text on button to +1
	}

	public void UnPause(){
		Time.timeScale = 1;
		currentMenu.SetActive(false);
		//hud.SetActive(true);
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {
	public UnityEngine.Canvas pauseMenu;

	public virtual void Awake(){
		pauseMenu = GetComponentInParent<UnityEngine.Canvas>();
	}

	public void StartGame(){
		Application.LoadLevel(1);

	}

	public void ExitGame(){
		//exit the game
	}

	public void SkillUp(){
		//increase skill here
		//based on soul count
		//change text on button to +1
	}

	public void UnPause(){
		Time.timeScale = 1;
		pauseMenu.enabled = false;
	}
}

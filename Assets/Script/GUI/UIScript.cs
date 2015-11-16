using UnityEngine;
using System.Collections;

public class UIScript : MonoBehaviour {

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

	}
}

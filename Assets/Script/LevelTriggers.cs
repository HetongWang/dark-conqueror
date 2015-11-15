using UnityEngine;
using System.Collections;

public class LevelTriggers : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	// 01Trigger
		// A single Rotopolly appears from the left, attacks user but is very weak

		//02Trigger
		// A group of 3Rotopolly show up. Very easy to kill

		//03Trigger
		// 5 Catwolf appear from above. 2 on left, 3 on right. They are fast and furious.

		//04Trigger
		// When you enter the water, 3 rotopolly pop out of the water to fight you. 
		// 2 wasps also start coming from the right to attack you.

		//05Trigger
		// A large group of Rotopolly come running at you from the right and above you!
		// There are 5 above and 5 below.

		//06Trigger
		// A large swarm of wasps start flying at you from a beehive. 
		// You must destroy the hive to stop them!
		// (this is optional, no need to make hive for the alpha build, just lots of wasps appearing is fine)

		//07Trigger
		// When you hit this trigger, all enemy types start coming towards you from the right.
		// 3 Rotopolly, 4 wasp, 2 wolf, and one giant rotopolly.
		// Once defeated a tree on the right side of the group starts glowing
		// You must destroy that tree to trigger the boss fight.
		// (tree destruction is optional for alpha)

		//08Trigger
		// When you reach this trigger, a short cutscene plays introducing the boss name
		// A tree will appear behind you to block you from exiting the boss stage
		// Boss fight continues until you win or lose.

		//the end

	}
}

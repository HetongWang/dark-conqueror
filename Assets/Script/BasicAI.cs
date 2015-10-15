using UnityEngine;
using System.Collections;

public enum actMode { attack, guard };

public class BasicAI : MonoBehaviour {

    protected GameObject[] players = GameObject.FindGameObjectsWithTag("player");
    public float viewRange;
    protected actMode mode;

    public GameObject seekPlayer()
    {
        GameObject res = players[0];
        float minDis = Vector3.Distance(players[0].transform.position, transform.position);

        for (int i = 1; i < players.Length; i++)
        {
            float dis = Vector3.Distance(players[i].transform.position, transform.position);
            if (dis < minDis)
            {
                minDis = dis;
                res = players[i];
            }
        }

        if (minDis <= viewRange)
            mode = actMode.attack;

        return res;
    }

    void Awake()
    {
        viewRange = 2;
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}
}

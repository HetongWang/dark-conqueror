using UnityEngine;
using System.Collections;

public enum moveMode { attack, guard };

public class BasicAI {

    protected GameObject[] players = GameObject.FindGameObjectsWithTag("player");
    protected moveMode mode;
    public bool moveToRight = true;
    protected Vector3 initPosition;
    protected Vector3 position;

    public float viewRange = 2;
    public float guardRange = 1;
    public float distantToPlayer = 0.3f;

    public float normalAttackRange = 0.3f;

    public BasicAI(Vector3 t)
    {
        position = t;
        initPosition = position;
        mode = moveMode.guard;
    }

    public GameObject seekPlayer()
    {
        GameObject res = players[0];
        float minDis = Vector3.Distance(players[0].transform.position, position);

        for (int i = 1; i < players.Length; i++)
        {
            float dis = Vector3.Distance(players[i].transform.position, position);
            if (dis < minDis)
            {
                minDis = dis;
                res = players[i];
            }
        }

        if (minDis <= viewRange)
            mode = moveMode.attack;

        return res;
    }

    protected float guardMovement()
    {
        float res = -1;
        if (moveToRight)
            res = 1;

        if (Mathf.Abs(position.x - initPosition.x) > guardRange)
            moveToRight = !moveToRight;

        return res;
    }

    protected float attactMovement(GameObject player)
    {
        float res = 0;

        float dis = position.x - player.transform.position.x;
        if (Mathf.Abs(dis) > distantToPlayer)
        {
            if (dis > 0)
                res = -1;
            else if (dis < 0)
                res = 1;
        }
        return res;
    }
	
	// Update is called once per frame
	public virtual float horMove (Vector3 pos) {
        position = pos;
        GameObject player = seekPlayer();
        float movement = 0;

        switch (mode)
        {
            case moveMode.attack:
                movement = attactMovement(player);
                break;
            case moveMode.guard:
                movement = guardMovement();
                break;
        }

        return movement;
	}
}

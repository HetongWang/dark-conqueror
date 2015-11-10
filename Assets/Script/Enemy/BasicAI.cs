using UnityEngine;
using System.Collections;


public class BasicAI {
    protected Character person;
    protected enum moveMode { attack, guard };

    protected GameObject[] players;
    protected moveMode movementMode;
    protected bool moveToRight = true;
    protected Vector3 initPosition;

    public float viewRange = 13;
    public float guardRange = 3;
    public float distantToPlayer = 1f;
    public GameObject targetPlayer;

    public BasicAI(Character person)
    {
        this.person = person;
        initPosition = person.transform.position;
        players = GameObject.FindGameObjectsWithTag("Player");
        movementMode = moveMode.guard;
        seekPlayer();
    }

    public GameObject seekPlayer()
    {
        if (players.Length == 0 || players[0] == null)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length == 0)
                return null;
        }

        GameObject res = players[0];
        targetPlayer = players[0];
        float minDis = Vector3.Distance(players[0].transform.position, person.transform.position);

        for (int i = 1; i < players.Length; i++)
        {
            float dis = Vector3.Distance(players[i].transform.position, person.transform.position);
            if (dis < minDis)
            {
                minDis = dis;
                res = targetPlayer = players[i];
            }
        }

        if (minDis <= viewRange)
            movementMode = moveMode.attack;
        else
        {
            if (movementMode == moveMode.attack)
                initPosition = person.transform.position;
            movementMode = moveMode.guard;
        }

        return res;
    }

    protected float guardMovement()
    {
        float res;
        if (Mathf.Abs(person.transform.position.x - initPosition.x) > guardRange)
            moveToRight = !moveToRight;

        if (moveToRight)
            res = 1;
        else
            res = -1;

        return res;
    }

    protected float attackMovement(GameObject player)
    {
        if (player == null)
            return 0;

        float res = 0;
        float dis = person.transform.position.x - player.transform.position.x;
        if (Mathf.Abs(dis) > distantToPlayer)
        {
            if (dis > 0)
                res = -1;
            else if (dis < 0)
                res = 1;
        }
        else
        {
            if (dis > 0.2)
                res = -0.01f;
            else if (dis < 0.2)
                res = 0.01f;
        }
        return res;
    }
	
    /// <summary>
    /// Calculate next movement
    /// </summary>
    /// <returns>Movement direction and speed</returns>
    public float horMove ()
    {
        GameObject player = seekPlayer();
        float movement = 0;

        switch (movementMode)
        {
            case moveMode.attack:
                movement = attackMovement(player);
                break;
            case moveMode.guard:
                movement = guardMovement();
                break;
        }

        return movement;
	}

    public virtual string update()
    {
        seekPlayer();
        return "move";
    }
}

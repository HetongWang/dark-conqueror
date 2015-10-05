using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public float xMargin = 0.3f;
    public float yMargin = 0.3f;
    public Vector3 velocity = Vector3.zero;
    public float delay = 0.2f;

    public Vector2 minXAndY = new Vector2(-2, 0);
    public Vector2 maxXAndY = new Vector2(5, 5);

    private Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
    }

    bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
    }

    void LateUpdate()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if (CheckXMargin())
        {
            targetX = player.position.x;
        }

        if (CheckYMargin())
        {
            targetY = player.position.y;
        }

        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetX, targetY, transform.position.z), ref velocity, delay);
    }
}

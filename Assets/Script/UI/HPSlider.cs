using UnityEngine;
using System.Collections;

public class HPSlider : MonoBehaviour {

    private Player player;
    public float value;
    public float initValue;

    UnityEngine.UI.Image bar;
    private Vector3 barScale;

    void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        bar = GetComponent<UnityEngine.UI.Image>();
        initValue = PlayerSet.Instance.hp;
        barScale = transform.localScale;
    }

	void Update ()
    {
        value = player.hp;
        // Set the health bar's colour to proportion of the way between green and red based on the player's health.
        bar.color = Color.Lerp(Color.green, Color.red, 1 - value / initValue);

        // Set the scale of the value bar to be proportional to the player's value.
        transform.localScale = new Vector3(barScale.x * value / initValue, 1, 1);

    }
}

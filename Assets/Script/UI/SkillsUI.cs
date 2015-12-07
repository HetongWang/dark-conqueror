using UnityEngine;
using System.Collections;

public class SkillsUI : MonoBehaviour
{
    protected Player pc;
	public int normalAttack = 13;
	public int eruptionFire= 6;
    UnityEngine.UI.Text text;

    public virtual void Awake()
    {
        pc = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        text = GetComponentInChildren<UnityEngine.UI.Text>();
    }

    void Update()
    {
		if(this.name == "SkillButtonNormal"){
			text.text = "" + pc.normalAttackLevel;
		} else if(this.name == "SkillButtonErupt"){
			text.text = "" + pc.setting.eruptionFireTimes;
		} else if (this.name == "SkillCostTextNormal"){
			text.text = "Cost: " + normalAttack + " souls";
		} else if (this.name == "SkillCostTextErupt"){
			text.text = "Cost: " + eruptionFire + " souls";
		}
    }

	public void UpdateNormalAttack(){
		if (pc.souls >= normalAttack && pc.normalAttackLevel < 2)
		{
			pc.normalAttackLevel += 1;
			pc.souls -= normalAttack;
		}
	}

	public void UpdateEruptAttack(){
		if (pc.souls >= eruptionFire)
		{
			pc.setting.eruptionFireTimes += 1;
			pc.setting.eruptionFire.actDuration += 0.2f / (pc.setting.eruptionFireTimes - 2);
			pc.souls -= eruptionFire;
		}
	}

	void OnDisable(){
		Time.timeScale = 1;
	}

	void OnDestroy()
	{
		Time.timeScale = 1;
	}
}

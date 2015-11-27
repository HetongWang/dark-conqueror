using UnityEngine;
using System.Collections;

public class NormalAttack : BasicAttack {

    protected int phase = 0;
    protected int level = 1;
    private Player owner;

    public override void Awake()
    {
        base.Awake();
        targetTag.Add("Enemy");
    }

    void Start()
    {
        owner = (Player)_owner;
        Destroy(gameObject, setting.attackDuration);
    }

    public override void getDemage(Collider2D col)
    {
        base.getDemage(col);
        Character c = col.gameObject.GetComponent<Character>();
        if (phase == 2 && level > 1)
            burnAttack(c);
    }

    public void setPhase(int phase)
    {
        this.phase = phase;
    }

    public void setLevel(int level)
    {
        this.level = level;
    }

    protected void burnAttack(Character c)
    {
        c.statusController.addStatus(new BurnStatus(c, owner.setting.normalAttackBurn));
    }
}

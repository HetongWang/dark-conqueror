using UnityEngine;
using System.Collections;

class RotopollyAttack : EnemyCommonAttack
{
    private Rotopolly owner;

    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        owner = (Rotopolly)_owner;
    }

    void Update()
    {
        setting.damage = owner.setting.run.damage;
        Debug.Log(setting.damage);
        Rotopolly r = (Rotopolly)_owner;
        if (!r.couldRun)
        {
            Destroy(gameObject);
        }
    }

    public override void getDemage(Collider2D col)
    {
        base.getDemage(col);
        StartCoroutine(clearHurtChar());
    }

    public IEnumerator clearHurtChar()
    {
        yield return new WaitForSeconds(0.5f);
        hurtChar.Clear();
        yield break;
    }
}

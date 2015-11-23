using UnityEngine;
using System.Collections;

class RotopollyAttack : EnemyCommonAttack
{
    public override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        setting.damage = Mathf.Abs(owner.body.velocity.x) / RotopollySet.Instance.runSpeed * RotopollySet.Instance.run.damage;
        Rotopolly r = (Rotopolly)owner;
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

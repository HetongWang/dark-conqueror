using UnityEngine;

public class BurnStatusSet : Singleton<BurnStatusSet>
{
    public float duration = 3f;
    public float demage = 0.5f;
    public bool canOverlay = false;
}

public class BurnStatus : StatusEffect
{
    public BurnStatus(Character person) : base(person)
    {
        duration = BurnStatusSet.Instance.duration;
    }

    public override void effect()
    {
        person.getDemage(BurnStatusSet.Instance.demage * Time.deltaTime);
    }
}
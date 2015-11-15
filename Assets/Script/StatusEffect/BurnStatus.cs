using UnityEngine;

public class BurnStatus : StatusEffect
{
    public struct Setting
    {
        public float damage, duration;
        public int maxOverlay;
    }

    protected float damage;

    public BurnStatus( Character person, Setting setting) : base(person)
    {
        duration = setting.duration;
        damage = setting.damage;
        maxOverlay = setting.maxOverlay;
    }

    public override void effect()
    {
        person.getDemage(damage * Time.deltaTime);
    }
}
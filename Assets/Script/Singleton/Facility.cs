using UnityEngine;

public class FloatRange
{
    public float min, max;
    public FloatRange(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

    public float random()
    {
        return Random.Range(min, max);
    }
}
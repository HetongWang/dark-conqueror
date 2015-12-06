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

public class IntRange
{
    public int min, max;
    public IntRange(int min, int max)
    {
        this.min = min;
        this.max = max;
    }

    public float random()
    {
        return Random.Range(min, max + 1);
    }
}
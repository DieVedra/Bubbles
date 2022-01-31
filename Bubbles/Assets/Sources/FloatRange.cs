using System;
using Random = UnityEngine.Random;

[Serializable]
public struct FloatRange
{
    private float _min, _max;

    public FloatRange(float min, float max)
    {
        _min = min;
        _max = max;
    }
    public float RandomValueInRange
    {
        get => Random.Range(_min, _max);
    }
}


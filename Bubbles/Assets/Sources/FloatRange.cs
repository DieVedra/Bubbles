using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct FloatRange
{
    [SerializeField] private float _min, _max;

    public float Min => _min;
    public float Max => _max;

    public float RandomValueInRange
    {
        get => Random.Range(_min, _max);
    }
    public FloatRange(float value)
    {
        _max = _min = value;
    }
    public FloatRange(float min, float max)
    {
        _min = min;
        _max = max;
    }
}


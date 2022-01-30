using System;
using UnityEngine;

public class Difficulty
{
    private AnimationCurve _difficultyCurve;
    private float _currentDifficulty;
    public Action<float> OnChangeDifficulty;
    public Difficulty(AnimationCurve difficultyCurve)
    {
        _difficultyCurve = difficultyCurve;
    }
    public void ChangeDifficulty(int score)
    {
        _currentDifficulty = _difficultyCurve.Evaluate((float)score);
        OnChangeDifficulty?.Invoke(_currentDifficulty);
    }
}

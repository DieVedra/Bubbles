using UnityEngine;

public class DifficultyHandler
{
    private AnimationCurve _curveScale;
    private AnimationCurve _curveSpeed;
    private FloatRange _floatRange;
    private float _currentDifficulty = 0f;
    private float _rangeNumberSpeed;
    private float _rangeNumberScale;
    private float _variable;
    public DifficultyHandler(AnimationCurve curveScale, AnimationCurve curveSpeed, float rangeNumberSpeed, float rangeNumberScale)
    {
        _curveScale = curveScale;
        _curveSpeed = curveSpeed;
        _rangeNumberSpeed = rangeNumberSpeed;
        _rangeNumberScale = rangeNumberScale;
    }
    public void SetDifficulty(float currentDifficulty)
    {
        _currentDifficulty = currentDifficulty;
    }
    public Vector2 GetScale()
    {
        _variable = _curveScale.Evaluate(CountsNumberFromRange(_currentDifficulty, _rangeNumberScale));
        return new Vector2(_variable, _variable);
    }
    public float GetSpeed()
    {
        return _curveSpeed.Evaluate(CountsNumberFromRange(_currentDifficulty, _rangeNumberSpeed));
    }
    private float CountsNumberFromRange(float difficulty, float number)
    {
        _floatRange = new FloatRange(difficulty, difficulty += number);
        return _floatRange.RandomValueInRange;
    }
}

using System;
using UnityEngine;

public class PercentageMode
{
    private FloatRange _floatRange = new FloatRange(0f, 100f);
    private PointGenerator _pointGenerator;
    private Range _usualRangePercent;
    private Range _accelerateRangePercent;
    private Range _jumpingRangePercent;
    private Strategy _strategy;
    private float _currentUsualPercent;
    private float _currentAccelerationPercent;
    private float _currentJumpingPercent;
    private float _sumUsualAndJumpingPercent;
    private float _sumUsualAndJumpingAndAccelerationPercent;
    private float _randomValue;
    private float _modeVariableFractional;
    public PercentageMode(PointGenerator pointGenerator, Range usualStrategyPercent, Range accelerateStrategyPercent = null, Range jumpingStrategyPercent = null)
    {
        _usualRangePercent = usualStrategyPercent;
        _accelerateRangePercent = accelerateStrategyPercent;
        _jumpingRangePercent = jumpingStrategyPercent;
        _pointGenerator = pointGenerator;
    }

    public Strategy AnalizesMode(float modeVariable, float speed)
    {
        CalculatesFractionalPart(modeVariable);
        CurrentPercent(_modeVariableFractional);
        _randomValue = _floatRange.RandomValueInRange;

        _sumUsualAndJumpingAndAccelerationPercent = _currentUsualPercent + _currentJumpingPercent + _currentAccelerationPercent;
        _sumUsualAndJumpingPercent = _currentUsualPercent + _currentJumpingPercent;

        if ((_sumUsualAndJumpingAndAccelerationPercent > 100f) == false)
        {
            if (_randomValue <= _currentUsualPercent)
            {
                _strategy = new UsualStrategy(speed);
            }
            else if (_randomValue > _currentUsualPercent && _randomValue <= _sumUsualAndJumpingPercent)
            {
                _strategy = new JumpingStrategy(speed, _pointGenerator);
            }
            else if (_randomValue > _sumUsualAndJumpingPercent && _randomValue <= _sumUsualAndJumpingAndAccelerationPercent)
            {
                _strategy = new AcceleratingStrategy(speed);
            }
            else throw new ArgumentOutOfRangeException();
        }
        else throw new ArgumentOutOfRangeException();

        return _strategy;
    }
    private void CalculatesFractionalPart(float modeVariable)
    {
        _modeVariableFractional = modeVariable - (int)modeVariable;
        if (modeVariable > 0f && _modeVariableFractional == 0f)
        {
            _modeVariableFractional = 1f;
        }
    }
    private void CurrentPercent(float modeVariable)
    {
        _currentUsualPercent = _usualRangePercent == null ? 0f : _usualRangePercent.CountsCurrentPercentage(modeVariable);
        _currentAccelerationPercent = _accelerateRangePercent == null ? 0f : _accelerateRangePercent.CountsCurrentPercentage(modeVariable);
        _currentJumpingPercent = _jumpingRangePercent == null ? 0f : _jumpingRangePercent.CountsCurrentPercentage(modeVariable);
    }
}
public class Range
{
    private float _begin;
    private float _end;
    public Range(float begin = 0, float end = 0)
    {
        _begin = begin;
        _end = end;
    }
    public float CountsCurrentPercentage(float number)
    {
        return Mathf.Lerp(_begin, _end, number);
    }
}

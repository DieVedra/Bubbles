using UnityEngine;

public class StrategyProvider
{
    private AnimationCurve _curveStrategyProvide;
    private float _currentDifficulty = 0f;
    private float _modeVariable;
    private PercentageMode _percentageModeA;
    private PercentageMode _percentageModeB;
    private PercentageMode _percentageModeC;
    private PercentageMode _percentageModeD;
    private Strategy _lastStrategy;
    private PointGenerator _pointGenerator;
    public StrategyProvider(AnimationCurve curveStrategyProvide, PointGenerator pointGenerator)
    {
        _curveStrategyProvide = curveStrategyProvide;
        _pointGenerator = pointGenerator;
        _percentageModeA = new PercentageMode(pointGenerator, new Range(100f, 90f), new Range(0f, 5f), new Range(0f, 5f)); // хз что делать мб вынести куда эти параметры сложности хуй пайми не знаю куда
        _percentageModeB = new PercentageMode(pointGenerator, new Range(90f, 70f), new Range(5f, 15f), new Range(5f, 15f));
        _percentageModeC = new PercentageMode(pointGenerator, new Range(70f, 55f), new Range(15f, 30f), new Range(15f, 15f));
        _percentageModeD = new PercentageMode(pointGenerator, new Range(55f, 40f), new Range(30, 35), new Range(15,25));
    }
    public void SetDifficulty(float currentDifficulty)
    {
        _currentDifficulty = currentDifficulty;
    }
    public Strategy GetStrategy(float speed)
    {
        _modeVariable = _curveStrategyProvide.Evaluate(_currentDifficulty);

        if (_modeVariable >= 0f && _modeVariable < 1f)
        {
            _lastStrategy =_percentageModeA.AnalizesMode(_modeVariable, speed);
        }
        else if(_modeVariable >= 1f && _modeVariable < 2f)
        {
            _lastStrategy =_percentageModeB.AnalizesMode(_modeVariable, speed);
        }
        else if(_modeVariable >= 2f && _modeVariable < 3f)
        {
            _lastStrategy =_percentageModeC.AnalizesMode(_modeVariable, speed);
        }
        else if(_modeVariable >= 3f)
        {
            _lastStrategy =_percentageModeD.AnalizesMode(_modeVariable, speed);
        }
        return _lastStrategy;
    }
}



using UnityEngine;

public class AcceleratingStrategy : Strategy
{
    private const float COUNT = 0.08f;
    private float _speed;
    private float _accelerate = COUNT + Time.deltaTime;
    private bool _isDetect = false;
    public AcceleratingStrategy(float speed)
    {
        _speed = speed;
    }
    public override void StrategyBehaviorMove(Comet comet)
    {
        if (_isDetect == true)
        {
            _speed += _accelerate;
        }
        base.Move(comet, comet.Position + Vector3.down * Time.deltaTime * _speed);
    }
    public override void StrategyBehaviorDetect(Comet comet)
    {
        if (_isDetect == false)
        {
            base.OnCkickSound?.Invoke();
            _isDetect = true;
            return;
        }
        base.ActivateParticles(comet.Particle, comet.Position, comet.Color, comet.Scale);
        base.OnParticleSound1?.Invoke();
        base.OnRemakeComet?.Invoke(comet);
    }
    public override void Accept(IStrategyVisitor strategyVisitor, Comet comet)
    {
        strategyVisitor.Visit(this, comet);
    }
}

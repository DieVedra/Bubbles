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
    public override void StrategyBehaviorMove(Ball ball)
    {
        if (_isDetect == true)
        {
            _speed += _accelerate;
        }
        base.Move(ball, ball.Position + Vector3.down * Time.deltaTime * _speed);
    }
    public override void StrategyBehaviorDetect(Ball ball)
    {
        if (_isDetect == false)
        {
            base.OnCkickSound?.Invoke();
            _isDetect = true;
            return;
        }
        base.ActivateParticles(ball.Particle, ball.Position, ball.Color, ball.Scale);
        base.OnParticleSound1?.Invoke();
        base.OnRemakeBall?.Invoke(ball);
    }
    public override void Accept(IStrategyVisitor strategyVisitor, Ball ball)
    {
        strategyVisitor.Visit(this, ball);
    }
}

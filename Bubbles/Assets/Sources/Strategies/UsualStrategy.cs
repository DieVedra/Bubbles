using UnityEngine;

public class UsualStrategy : Strategy
{
    private float _speed;
    public UsualStrategy(float speed)
    {
        _speed = speed;
    }
    public override void StrategyBehaviorMove(Ball ball)
    {
        base.Move(ball, ball.Position + Vector3.down * Time.deltaTime * _speed);
    }
    public override void StrategyBehaviorDetect(Ball ball)
    {
        base.ActivateParticles(ball.Particle, ball.Position, ball.Color, ball.Scale);
        base.OnParticleSound1?.Invoke();
        base.OnRemakeBall?.Invoke(ball);
    }
    public override void Accept(IStrategyVisitor strategyVisitor, Ball ball)
    {
        strategyVisitor.Visit(this, ball);
    }
}

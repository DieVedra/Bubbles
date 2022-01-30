using UnityEngine;

public class UsualStrategy : Strategy
{
    private float _speed;
    public UsualStrategy(float speed)
    {
        _speed = speed;
    }
    public override void StrategyBehaviorMove(Comet comet)
    {
        base.Move(comet, comet.Position + Vector3.down * Time.deltaTime * _speed);
    }
    public override void StrategyBehaviorDetect(Comet comet)
    {
        base.ActivateParticles(comet.Particle, comet.Position, comet.Color, comet.Scale);
        base.OnParticleSound1?.Invoke();
        base.OnRemakeComet?.Invoke(comet);
    }
    public override void Accept(IStrategyVisitor strategyVisitor, Comet comet)
    {
        strategyVisitor.Visit(this, comet);
    }
}

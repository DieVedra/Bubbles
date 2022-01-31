using System;
using UnityEngine;

public abstract class Strategy
{
    public Action<Ball> OnRemakeBall;
    public Action OnCkickSound;
    public Action OnRemakeClickSound;
    public Action OnParticleSound1;
    protected void ActivateParticles(Particle particle, Vector3 position, Color color, Vector3 scale)
    {
        particle.SetScale(scale);
        particle.Painting(color);
        particle.UnHideMe();
        particle.TransferToPosition(position);
        particle.PlayParticle();
    }
    protected void Move(Ball ball, Vector3 position)
    {
        ball.MoveToPosition(position);
    }
    public void FallenBall(Ball ball)
    {
        ActivateParticles(ball.Particle, ball.Position, ball.Color, ball.Scale);
        OnRemakeBall?.Invoke(ball);
    }
    public abstract void StrategyBehaviorMove(Ball ball);
    public abstract void StrategyBehaviorDetect(Ball ball);
    public abstract void Accept(IStrategyVisitor strategyVisitor, Ball ball);
}

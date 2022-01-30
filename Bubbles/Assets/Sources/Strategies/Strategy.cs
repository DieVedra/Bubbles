using System;
using UnityEngine;

public abstract class Strategy
{
    public Action<Comet> OnRemakeComet;
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
    protected void Move(Comet comet, Vector3 position)
    {
        comet.MoveToPosition(position);
    }
    public void FallenComet(Comet comet)
    {
        ActivateParticles(comet.Particle, comet.Position, comet.Color, comet.Scale);
        OnRemakeComet?.Invoke(comet);
    }
    public abstract void StrategyBehaviorMove(Comet comet);
    public abstract void StrategyBehaviorDetect(Comet comet);
    public abstract void Accept(IStrategyVisitor strategyVisitor, Comet comet);
}

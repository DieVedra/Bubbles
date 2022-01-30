using System.Collections.Generic;

public class Mover
{
    private readonly StrategyVisitorMove _strategyVisitorMove = new StrategyVisitorMove();
    private IReadOnlyList<Comet> _comets;
    public Mover(IReadOnlyList<Comet> comets)
    {
        _comets = comets;
    }
    public void MoveUpdate()
    {
        for (int i = 0; i < _comets.Count; i++)
        {
            if (_comets[i].IsLaunched)
            {
                StrategyHandler(_comets[i].Strategy, _comets[i]);
            }
        }
    }
    private void StrategyHandler(Strategy strategy, Comet comet)
    {
        strategy.Accept(_strategyVisitorMove, comet);
    }

    private class StrategyVisitorMove : IStrategyVisitor
    {
        public void Visit(UsualStrategy usualStrategy, Comet comet)
        {
            usualStrategy.StrategyBehaviorMove(comet);
        }
        public void Visit(JumpingStrategy jumpingStrategy, Comet comet)
        {
            jumpingStrategy.StrategyBehaviorMove(comet);
        }
        public void Visit(AcceleratingStrategy acceleratingStrategy, Comet comet)
        {
            acceleratingStrategy.StrategyBehaviorMove(comet);
        }

    }
}

using System.Collections.Generic;

public class Mover
{
    private readonly StrategyVisitorMove _strategyVisitorMove = new StrategyVisitorMove();
    private IReadOnlyList<Ball> _balls;
    public Mover(IReadOnlyList<Ball> balls)
    {
        _balls = balls;
    }
    public void MoveUpdate()
    {
        for (int i = 0; i < _balls.Count; i++)
        {
            if (_balls[i].IsLaunched)
            {
                StrategyHandler(_balls[i].Strategy, _balls[i]);
            }
        }
    }
    private void StrategyHandler(Strategy strategy, Ball ball)
    {
        strategy.Accept(_strategyVisitorMove, ball);
    }

    private class StrategyVisitorMove : IStrategyVisitor
    {
        public void Visit(UsualStrategy usualStrategy, Ball ball)
        {
            usualStrategy.StrategyBehaviorMove(ball);
        }
        public void Visit(JumpingStrategy jumpingStrategy, Ball ball)
        {
            jumpingStrategy.StrategyBehaviorMove(ball);
        }
        public void Visit(AcceleratingStrategy acceleratingStrategy, Ball ball)
        {
            acceleratingStrategy.StrategyBehaviorMove(ball);
        }

    }
}

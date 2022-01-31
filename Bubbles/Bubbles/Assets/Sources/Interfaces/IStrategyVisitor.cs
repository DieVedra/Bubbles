
public interface IStrategyVisitor
{
    void Visit(UsualStrategy usualStrategy, Ball ball);
    void Visit(JumpingStrategy jumpingStrategy, Ball ball);
    void Visit(AcceleratingStrategy acceleratingStrategy, Ball ball);
}

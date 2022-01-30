
public interface IStrategyVisitor
{
    void Visit(UsualStrategy usualStrategy, Comet comet);
    void Visit(JumpingStrategy jumpingStrategy, Comet comet);
    void Visit(AcceleratingStrategy acceleratingStrategy, Comet comet);
}

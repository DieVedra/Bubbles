
public class StrategyVisitor : IStrategyVisitor
{
    public virtual void Visit(UsualStrategy usualStrategy, Comet comet){}
    public virtual void Visit(JumpingStrategy jumpingStrategy, Comet comet){}
    public virtual void Visit(AcceleratingStrategy acceleratingStrategy, Comet comet){}
}

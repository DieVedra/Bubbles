using UnityEngine;

public class JumpingStrategy : Strategy
{
    private const float COUNT = 10f;
    private Vector3 _pointStartJump;
    private Vector3 _pointEndJump;
    private PointGenerator _pointGenerator;
    private float _speed;
    private float _speedJump => _speed * COUNT * Time.deltaTime;
    private float _t = 0f;
    private bool _isJump = false;
    private bool _isReclaim = false;
    public JumpingStrategy(float speed, PointGenerator pointGenerator)
    {
        _speed = speed;
        _pointGenerator = pointGenerator;
    }
    public override void StrategyBehaviorMove(Ball ball)
    {
        if (_isJump == false)
        {
            base.Move(ball, ball.Position + Vector3.down * Time.deltaTime * _speed);
        }
        else if (_isJump == true)
        {
            Jump(ball);
        }
    }
    public override void StrategyBehaviorDetect(Ball ball)
    {
        if (_isJump == false && _isReclaim == false)
        {
            base.OnCkickSound?.Invoke();
            _pointStartJump = ball.Position;
            _pointEndJump = _pointGenerator.GetPointToJump(ball.Scale, ball.PointStart, ball.Position);

            _isJump = true;
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
    private void Jump(Ball ball)
    {
        Move(ball, Vector3.Lerp(_pointStartJump, _pointEndJump, _t));
        _t += _speedJump;
        if (_t >= 0.99f)
        {
            _t = 1f;
            _isReclaim = true;
            _isJump = false;
        }
    }
}

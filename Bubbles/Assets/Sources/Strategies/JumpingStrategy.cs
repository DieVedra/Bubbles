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
    public override void StrategyBehaviorMove(Comet comet)
    {
        if (_isJump == false)
        {
            base.Move(comet, comet.Position + Vector3.down * Time.deltaTime * _speed);
        }
        else if (_isJump == true)
        {
            Jump(comet);
        }
    }
    public override void StrategyBehaviorDetect(Comet comet)
    {
        if (_isJump == false && _isReclaim == false)
        {
            base.OnCkickSound?.Invoke();
            _pointStartJump = comet.Position;
            _pointEndJump = _pointGenerator.GetPointToJump(comet.Scale, comet.PointStart, comet.Position);

            _isJump = true;
            return;
        }
        base.ActivateParticles(comet.Particle, comet.Position, comet.Color, comet.Scale);
        base.OnParticleSound1?.Invoke();
        base.OnRemakeComet?.Invoke(comet);
    }
    public override void Accept(IStrategyVisitor strategyVisitor, Comet comet)
    {
        strategyVisitor.Visit(this, comet);
    }
    private void Jump(Comet comet)
    {
        Move(comet, Vector3.Lerp(_pointStartJump, _pointEndJump, _t));
        _t += _speedJump;
        if (_t >= 0.99f)
        {
            _t = 1f;
            _isReclaim = true;
            _isJump = false;
        }
    }
}

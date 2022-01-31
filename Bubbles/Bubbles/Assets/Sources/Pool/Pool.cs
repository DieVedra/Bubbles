using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private const int MAX_COUNT_ON_SCREEN = 3;
    [SerializeField] private Factory _factory;
    [SerializeField] private Transform _leftBorderPoint;
    [SerializeField] private Transform _rightBorderPoint;
    private Transform _transform;
    private Difficulty _difficulty;
    private Mover _mover;
    private Sound _sound;
    private StrategyVisitorDetect _strategyVisitorDetect = new StrategyVisitorDetect();
    private List<Ball> _balls = new List<Ball>();
    private List<Particle> _particles = new List<Particle>();
    private int _numberRunning;
    public IReadOnlyList<Ball> Balls => _balls;
    public Mover Mover => _mover;

    public void Init(Difficulty difficulty, Sound sound)
    {
        _transform = GetComponent<Transform>();
        _sound = sound;
        _difficulty = difficulty;
        _mover = new Mover(Balls);
        _factory.Init(new PointGenerator(_leftBorderPoint.position, _rightBorderPoint.position));
        _difficulty.OnChangeDifficulty += _factory.DifficultyHandler.SetDifficulty;
        _difficulty.OnChangeDifficulty += _factory.StrategyProvider.SetDifficulty;
    }

    public void LaunchBall()
    {
        if (_balls.Count == 0)
        {
            Generate();
            return; //exit the method when generating first object
        }
        if (RelaunchBall() == false)
        {
            Generate(); // create new object if there are no available
        }
    }
    public void DetectBall(Ball ball)
    {
        ball.Strategy.OnCkickSound += _sound.PlayTouch1;
        ball.Strategy.OnParticleSound1 += _sound.PlayExplosion1;
        StrategyHandler(ball.Strategy, ball);
        LaunchByTouch();
        ball.Strategy.OnCkickSound -= _sound.PlayTouch1;
        ball.Strategy.OnParticleSound1 -= _sound.PlayExplosion1;
    }
    public void FallenBall(Ball ball)
    {
        _sound.PlayExplosion2();
        ball.Strategy.FallenBall(ball);
    }
    public void ResetPool()
    {
        for (int i = 0; i < _balls.Count; i++)
        {
            Terminate(_balls[i].gameObject);
            Terminate(_particles[i].gameObject);
        }
        _balls.Clear();
        _particles.Clear();
    }
    private void LaunchByTouch()    
    {
        _numberRunning = 0;
        for (int i = 0; i < _balls.Count; i++)
        {
            if (_balls[i].IsLaunched == true)
            {
                _numberRunning++;
            }
        }
        if (_numberRunning > MAX_COUNT_ON_SCREEN)
        {
            return;
        }
        else
        {
            LaunchBall();
        }
    }
    private bool RelaunchBall()
    {
        for (int i = 0; i < _balls.Count; i++)
        {
            if (_balls[i].IsLaunched == false)
            {
                _balls[i].UnHideMe();
                _balls[i].IsLaunched = true;
                return true; // do not generate new object
            }
        }
        return false; // generate new object
    }
    private void Generate()
    {
        GenerateBall(GenetateParticle());
    }
    private Particle GenetateParticle()
    {
        Particle particle = _factory.CreateParticle(_transform.position, _transform);
        AddInList(_particles, particle);
        return particle;
    }
    private void GenerateBall(Particle particle)
    {
        Ball ball = _factory.CreateBall(_transform, particle);
        AddInList(_balls, ball);
    }
    private void AddInList<T>(List<T> list, T obj)
    {
        list.Add(obj);
    }
    private void Terminate(GameObject gameObject)
    {
        Destroy(gameObject);
    }
    private void StrategyHandler(Strategy strategy, Ball ball)
    {
        strategy.Accept(_strategyVisitorDetect, ball);
    }
    private class StrategyVisitorDetect : IStrategyVisitor
    {
        public void Visit(UsualStrategy usualStrategy, Ball ball)
        {
            usualStrategy.StrategyBehaviorDetect(ball);
        }
        public void Visit(JumpingStrategy jumpingStrategy, Ball ball)
        {
            jumpingStrategy.StrategyBehaviorDetect(ball);
        }
        public void Visit(AcceleratingStrategy acceleratingStrategy, Ball ball)
        {
            acceleratingStrategy.StrategyBehaviorDetect(ball);
        }
    }
}

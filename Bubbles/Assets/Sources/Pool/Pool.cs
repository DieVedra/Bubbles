using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private Factory _factory;
    [SerializeField] private Transform _leftBorderPoint;
    [SerializeField] private Transform _rightBorderPoint;
    private Transform _transform;
    private Difficulty _difficulty;
    private Mover _mover;
    private Sound _sound;
    private StrategyVisitorDetect _strategyVisitorDetect = new StrategyVisitorDetect();
    private List<Comet> _comets = new List<Comet>();
    private List<Particle> _particles = new List<Particle>();
    private int _numberRunning;
    private int _maxCountOnScreen = 3;
    public IReadOnlyList<Comet> Comets => _comets;
    public Mover Mover => _mover;

    public void Init(Difficulty difficulty, Sound sound)
    {
        _transform = GetComponent<Transform>();
        _sound = sound;
        _difficulty = difficulty;
        _mover = new Mover(Comets);
        _factory.Init(new PointGenerator(_leftBorderPoint.position, _rightBorderPoint.position));
        _difficulty.OnChangeDifficulty += _factory.DifficultyHandler.SetDifficulty;
        _difficulty.OnChangeDifficulty += _factory.StrategyProvider.SetDifficulty;
    }

    public void LaunchComet()//запускает по вызову таймера 
    {
        if (_comets.Count == 0)
        {
            Generate();
            return; //выйти из метода при генерации первого обьекта
        }
        if (RelaunchComet() == false)
        {
            Generate(); // создать обьект если нет свободных 
        }
    }
    public void DetectComet(Comet comet)
    {
        comet.Strategy.OnCkickSound += _sound.PlayTouch1;
        comet.Strategy.OnParticleSound1 += _sound.PlayExplosion1;
        StrategyHandler(comet.Strategy, comet);
        LaunchByTouch();
        comet.Strategy.OnCkickSound -= _sound.PlayTouch1;
        comet.Strategy.OnParticleSound1 -= _sound.PlayExplosion1;
    }
    public void FallenComet(Comet comet)
    {
        _sound.PlayExplosion2();
        comet.Strategy.FallenComet(comet);
    }
    public void ResetPool()
    {
        for (int i = 0; i < _comets.Count; i++)
        {
            Terminate(_comets[i].gameObject);
            Terminate(_particles[i].gameObject);
        }
        _comets.Clear();
        _particles.Clear();
    }
    private void LaunchByTouch()    
    {
        _numberRunning = 0;
        for (int i = 0; i < _comets.Count; i++)
        {
            if (_comets[i].IsLaunched == true)
            {
                _numberRunning++;
            }
        }
        if (_numberRunning > _maxCountOnScreen)    //чето придумать что б от сложности регулировалось
        {
            return;
        }
        else
        {
            LaunchComet();
        }
    }
    private bool RelaunchComet()
    {
        for (int i = 0; i < _comets.Count; i++)
        {
            if (_comets[i].IsLaunched == false) //если комета не запущена
            {
                _comets[i].UnHideMe(); //включает 
                _comets[i].IsLaunched = true; //помечает как запущенное
                return true; //не генерировать новый обьект
            }
        }
        return false; //генерировать новый обьект
    }
    private void Generate()
    {
        GenerateComet(GenetateParticle());
    }
    private Particle GenetateParticle()
    {
        Particle particle = _factory.CreateParticle(_transform.position, _transform);
        AddInList(_particles, particle);
        return particle;
    }
    private void GenerateComet(Particle particle)
    {
        Comet comet = _factory.CreateComet(_transform, _comets.Count, 5f, particle);
        AddInList(_comets, comet);
    }
    private void AddInList<T>(List<T> list, T obj)
    {
        list.Add(obj);
    }
    private void Terminate(GameObject gameObject)
    {
        Destroy(gameObject);
    }
    private void StrategyHandler(Strategy strategy, Comet comet)
    {
        strategy.Accept(_strategyVisitorDetect, comet);
    }
    private class StrategyVisitorDetect : IStrategyVisitor
    {
        public void Visit(UsualStrategy usualStrategy, Comet comet)
        {
            usualStrategy.StrategyBehaviorDetect(comet);
        }
        public void Visit(JumpingStrategy jumpingStrategy, Comet comet)
        {
            jumpingStrategy.StrategyBehaviorDetect(comet);
        }
        public void Visit(AcceleratingStrategy acceleratingStrategy, Comet comet)
        {
            acceleratingStrategy.StrategyBehaviorDetect(comet);
        }
    }
}

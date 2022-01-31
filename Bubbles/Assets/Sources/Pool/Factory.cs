using UnityEngine;

[CreateAssetMenu(fileName = "Factory", menuName = "Factory", order = 51)]
public class Factory : ScriptableObject
{
    [SerializeField] private Color _pink;
    [SerializeField] private Color _yellow;
    [SerializeField] private Color _blue;
    [SerializeField] private Color _green;
    [SerializeField] private Particle _particlePrefab;
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private AnimationCurve _curveScale;
    [SerializeField] private AnimationCurve _curveSpeed;
    [SerializeField] private AnimationCurve _curveStrategyProvide;
    [SerializeField] private float _rangeNumberSpeed;
    [SerializeField] private float _rangeNumberScale;
    private PaintingRoom _paintingRoom;
    private PointGenerator _pointGenerator;
    private DifficultyHandler _difficultyHandler;
    private StrategyProvider _strategyProvider;
    private Vector2 _scaleBall;
    private Vector3 _pointStartBall;
    public DifficultyHandler DifficultyHandler => _difficultyHandler;
    public StrategyProvider StrategyProvider => _strategyProvider;
    public void Init(PointGenerator pointGenerator)
    {
        _pointGenerator = pointGenerator;
        _paintingRoom = new PaintingRoom(_pink, _yellow, _blue, _green);
        _difficultyHandler = new DifficultyHandler(_curveScale, _curveSpeed, _rangeNumberSpeed , _rangeNumberScale);
        _strategyProvider = new StrategyProvider(_curveStrategyProvide, pointGenerator);
    }
    public Ball CreateBall(Transform parent, Particle particle)
    {
        ScaleAndPointStartInit();

        Ball ball = CreateProduct(_ballPrefab, _pointStartBall, parent);

        ball.SetStartPoint(_pointStartBall);
        ball.SetScale(_scaleBall);

        ball.SetParticle(particle);

        ball.Painting(_paintingRoom.GetColor(ball.Color));
        GivesStrategy(ball);
        ball.IsLaunched = true;
        return ball;
    }
    public void RemakeBall(Ball ball)
    {
        ball.Strategy.OnRemakeBall -= RemakeBall;
        ball.IsLaunched = false;
        ball.HideMe();

        ScaleAndPointStartInit();

        ball.SetStartPoint(_pointStartBall);
        ball.SetScale(_scaleBall);


        ball.TransferToPosition(_pointStartBall);
        GivesStrategy(ball);

        ball.Painting(_paintingRoom.GetColor(ball.Color));
    }
    public Particle CreateParticle(Vector3 spawnPoint, Transform parent)
    {
        Particle particle = CreateProduct(_particlePrefab, spawnPoint, parent);
        particle.HideMe();
        return particle;
    }
    private T CreateProduct<T>(T prefab, Vector3 spawnPoint, Transform parent) where T : MonoBehaviour
    {
        T instance = Instantiate(prefab, spawnPoint, Quaternion.identity, parent);
        return instance;
    }
    private void GivesStrategy(Ball ball)
    {
        Strategy strategy = _strategyProvider.GetStrategy(_difficultyHandler.GetSpeed());
        strategy.OnRemakeBall += RemakeBall;
        ball.SetStrategy(strategy);
    }
    private void ScaleAndPointStartInit()
    {
        _scaleBall = _difficultyHandler.GetScale();
        _pointStartBall = _pointGenerator.GetStartPoint(_scaleBall);
    }
}

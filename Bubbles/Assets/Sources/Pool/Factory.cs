using UnityEngine;

[CreateAssetMenu(fileName = "Factory", menuName = "Factory", order = 51)]
public class Factory : ScriptableObject
{
    [SerializeField] private Color _pink;
    [SerializeField] private Color _yellow;
    [SerializeField] private Color _blue;
    [SerializeField] private Color _green;
    [SerializeField] private Particle _particlePrefab;
    [SerializeField] private Comet _cometPrefab;
    [SerializeField] private AnimationCurve _curveScale;
    [SerializeField] private AnimationCurve _curveSpeed;
    [SerializeField] private AnimationCurve _curveStrategyProvide;
    [SerializeField] private float _rangeNumberSpeed;
    [SerializeField] private float _rangeNumberScale;
    private PaintingRoom _paintingRoom;
    private PointGenerator _pointGenerator;
    private DifficultyHandler _difficultyHandler;
    private StrategyProvider _strategyProvider;
    public DifficultyHandler DifficultyHandler => _difficultyHandler;
    public StrategyProvider StrategyProvider => _strategyProvider;
    public void Init(PointGenerator pointGenerator)
    {
        _pointGenerator = pointGenerator;
        _paintingRoom = new PaintingRoom(_pink, _yellow, _blue, _green);
        _difficultyHandler = new DifficultyHandler(_curveScale, _curveSpeed, _rangeNumberSpeed , _rangeNumberScale);
        _strategyProvider = new StrategyProvider(_curveStrategyProvide, pointGenerator);
    }
    public Comet CreateComet(Transform parent, int index, float damage, Particle particle)
    {
        Vector2 scale = _difficultyHandler.GetScale();
        Vector3 pointStart = _pointGenerator.GetStartPoint(scale);
        Comet comet = CreateProduct(_cometPrefab, pointStart, parent);
        comet.SetStartPoint(pointStart);
        comet.SetScale(scale);
        comet.SetParticle(particle);
        comet.SetDamage(damage);
        _paintingRoom.GetColor(comet);
        GivesStrategy(comet);
        comet.IsLaunched = true;
        return comet;
    }
    public void RemakeComet(Comet comet)
    {
        comet.Strategy.OnRemakeComet -= RemakeComet;
        float damage = 5f;
        comet.IsLaunched = false;
        comet.HideMe();
        Vector2 scale = _difficultyHandler.GetScale();
        Vector3 pointStart = _pointGenerator.GetStartPoint(scale);
        comet.SetStartPoint(pointStart);
        comet.TransferToPosition(pointStart);
        comet.SetDamage(damage);
        GivesStrategy(comet);
        _paintingRoom.GetColor(comet);
    }
    public Particle CreateParticle(Vector3 spawnPoint, Transform parent)
    {
        Particle particle = CreateProduct(_particlePrefab, spawnPoint, parent);
        particle.HideMe();
        return particle;
    }
    public T CreateProduct<T>(T prefab, Vector3 spawnPoint, Transform parent) where T : MonoBehaviour
    {
        T instance = Instantiate(prefab, spawnPoint, Quaternion.identity, parent);
        return instance;
    }
    private void GivesStrategy(Comet comet)
    {
        Strategy strategy = _strategyProvider.GetStrategy(_difficultyHandler.GetSpeed());
        strategy.OnRemakeComet += RemakeComet;
        comet.SetStrategy(strategy);
    }
}

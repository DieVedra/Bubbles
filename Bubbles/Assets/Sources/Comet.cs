using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class Comet : ObjectBeingLaunched
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private CircleCollider2D _circleCollider2D;
    private float _damage;
    private float _colliderMultiplier = 0.6f;
    private Particle _particle;
    private Strategy _currentStrategy;
    private Vector3 _pointStart;
    public float Damage => _damage;
    public Vector3 Position => _rigidbody2D.position;
    public Vector3 PointStart => _pointStart;
    public Color Color => _spriteRenderer.color;
    public Strategy Strategy=> _currentStrategy;
    public Vector3 Scale => _transform.localScale;
    public Particle Particle => _particle;
    [HideInInspector] public bool IsLaunched = false;
    private void Awake() 
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }
    public override void SetScale(Vector3 scale)
    {
        _transform.localScale = scale;
        _circleCollider2D.radius = scale.x * _colliderMultiplier;
    }
    public void SetDamage(float damage)
    {
        _damage = damage;
    }
    public void SetStrategy(Strategy strategy)
    {
        _currentStrategy = strategy;
    }
    public void SetParticle(Particle particle)
    {
        _particle = particle;
    }
    public void SetStartPoint(Vector3 point)
    {
        _pointStart = point;
    }
    public void MoveToPosition(Vector3 point)
    {
        _rigidbody2D.MovePosition(point);
    }
    // public void TransferToPosition(Vector3 point)
    // {
    //     transform.position = point;
    // }
    // public void HideMe()
    // {
    //     gameObject.SetActive(false);
    // }
    // public void UnHideMe()
    // {
    //     gameObject.SetActive(true);
    // }
    public override void Painting(Color color)
    {
        _spriteRenderer.color = color;
    }
}

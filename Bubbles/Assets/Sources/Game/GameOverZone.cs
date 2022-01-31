using System;
using UnityEngine;

public class GameOverZone : MonoBehaviour
{
    [SerializeField] private float _damage = 1f;
    private Ball _ball;
    public event Action<Ball> OnDeadZoneTriggered;
    public event Action<float> OnGiveDamage;
    public event Action OnExplotion;
    private void OnTriggerEnter2D(Collider2D ball) 
    {
        if(ball.TryGetComponent<Ball>(out _ball))
        {
            OnDeadZoneTriggered?.Invoke(_ball);
            OnGiveDamage?.Invoke(_damage);
            OnExplotion?.Invoke();
        }
    }
}

using System;
using UnityEngine;

public class GameOverZone : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curveDamageDeadZone;
    private Ball _ball;
    private float _currentDifficulty = 0f;
    public event Action<Ball> OnDeadZoneTriggered;
    public event Action<float> OnGiveDamage;
    public event Action OnExplotion;
    public void SetDifficulty(float currentDifficulty)
    {
        _currentDifficulty = currentDifficulty;
    }
    private void OnTriggerEnter2D(Collider2D ball) 
    {
        if(ball.TryGetComponent<Ball>(out _ball))
        {
            OnDeadZoneTriggered?.Invoke(_ball);
            OnGiveDamage?.Invoke(_curveDamageDeadZone.Evaluate(_currentDifficulty));
            OnExplotion?.Invoke();
        }
    }
}

using System;
using UnityEngine;

public class Player
{
    private float _health;
    private int _score = 0;
    public event Action OnPlayerDead;
    public event Action<int> OnPlayerDeadScore;
    public event Action<int> OnChangedScore;
    public event Action<float> OnChangedHealth;
    private bool _isAlive => _health > 0;
    private bool _isDead = false;
    public Player(float health)
    {
        _health = health;
    }
    public void TakeDamage(float damage)
    {
        if (_isAlive == true)
        {
            _health -= damage;
            OnChangedHealth?.Invoke(_health);
        }
        else
        {
            if (_isDead == false)
            {
                OnPlayerDead?.Invoke();
                OnPlayerDeadScore?.Invoke(_score);
                _isDead = true;
            }
        }
    }
    public void SetScore(Comet comet)
    {
        _score += 1; // потом поменять
        OnChangedScore?.Invoke(_score);
    }
    public void ResetPlayer(float value)
    {
        _score = 0;
        OnChangedScore?.Invoke(_score);
        _health = value;
        _isDead = false;
    }
}

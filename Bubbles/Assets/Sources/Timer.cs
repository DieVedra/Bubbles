using System;
// using UnityEngine;

public class Timer
{
    private float _time;
    public float TimeMax; // придумать как от сложности регулировать в диапазоне
    public event Action OnWakeUp;
    public Timer(float timeMax)
    {
        TimeMax = timeMax;
    }
    public void TimerUpdate(float deltaTime)
    {
        if (_time >= TimeMax)
        {
            OnWakeUp?.Invoke();
            // Debug.Log("OnWakeUp");
            _time = 0f;
        }
        _time += deltaTime;

    }
    public void ResetTimer()
    {
        _time = 0f;
    }
}

using System;

public class Timer
{
    private float _time;
    public readonly float TimeMax;
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
            _time = 0f;
        }
        _time += deltaTime;

    }
    public void ResetTimer()
    {
        _time = 0f;
    }
}

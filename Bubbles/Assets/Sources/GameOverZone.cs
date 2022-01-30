using System;
using UnityEngine;

public class GameOverZone : MonoBehaviour
{
    private Comet comet;
    public event Action<Comet> OnDeadZoneTriggered;
    public event Action<float> OnGiveDamage;
    public event Action OnExplotion;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.TryGetComponent<Comet>(out comet))
        {
            OnDeadZoneTriggered?.Invoke(comet);
            OnGiveDamage?.Invoke(comet.Damage);
            OnExplotion?.Invoke();
        }
    }
}

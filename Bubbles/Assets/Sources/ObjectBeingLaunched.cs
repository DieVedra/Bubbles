using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectBeingLaunched : MonoBehaviour
{
    private Transform _transform;
    
    public void TransferToPosition(Vector2 point)
    {
        transform.position = point;
    }
    public void HideMe()
    {
        gameObject.SetActive(false);
    }
    public void UnHideMe()
    {
        gameObject.SetActive(true);
    }
    public abstract void SetScale(Vector3 scale);
    public abstract void Painting(Color color);
}

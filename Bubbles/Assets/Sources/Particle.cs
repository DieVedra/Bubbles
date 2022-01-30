using UnityEngine;

public class Particle : ObjectBeingLaunched
{
    private ParticleSystem _tapParticle;
    private Transform _transform;
    private void Awake() 
    {
        _tapParticle = GetComponent<ParticleSystem>();
        _transform = GetComponent<Transform>();
    }
    // public void TransferToPosition(Vector2 point)
    // {
    //     transform.position = point;
    // }
    public void PlayParticle()
    {
        _tapParticle.Play();
    }
    public override void SetScale(Vector3 scale)
    {
        _transform.localScale = scale;
    }
    // public void HideMe()
    // {
    //     gameObject.SetActive(false);
    // }
    // public void UnHideMe()
    // {
    //     gameObject.SetActive(true);
    // }
    public override  void Painting(Color color)
    {
        ParticleSystem.MainModule main = _tapParticle.main;
        main.startColor = color;
    }
}

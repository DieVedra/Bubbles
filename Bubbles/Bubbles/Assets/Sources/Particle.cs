using UnityEngine;

public class Particle : ObjectBeingLaunched
{
    private ParticleSystem _tapParticle;
    private void Awake() 
    {
        _tapParticle = GetComponent<ParticleSystem>();
        base._transform = GetComponent<Transform>();
    }
    public void PlayParticle()
    {
        _tapParticle.Play();
    }
    public override void SetScale(Vector3 scale)
    {
        base._transform.localScale = scale;
    }
    public override void Painting(Color color)
    {
        ParticleSystem.MainModule main = _tapParticle.main;
        main.startColor = color;
    }
}

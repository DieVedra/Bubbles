using UnityEngine;

public class Sound
{
    private const string PARTICLE_EXPLOSION_CLIP1 = "AudioClips/ParticleExplosionClip1";
    private const string PARTICLE_EXPLOSION_CLIP2 = "AudioClips/ParticleExplosionClip2";
    private const string START_GAME_CLIP = "AudioClips/StartGameClip";
    private const string RESUME_GAME_CLIP = "AudioClips/ResumeGameClip";
    private const string PAUSE_GAME_CLIP = "AudioClips/PauseGameClip";
    private const string OVER_GAME_CLIP = "AudioClips/OverGameClip";
    private const string BALL_CLIP1 = "AudioClips/BallClip1";
    private AudioSource _audioSource;
    private MobileVibration _mobileVibration = new MobileVibration();
    private AudioClip _particleExplosionClip1;
    private AudioClip _particleExplosionClip2;
    private AudioClip _startGameClip;
    private AudioClip _resumeGameClip;
    private AudioClip _pauseGameClip;
    private AudioClip _overGameClip;
    private AudioClip _ballClip1;

    public Sound(AudioSource audioSource)
    {
        _audioSource = audioSource;
        _particleExplosionClip1 = Resources.Load<AudioClip>(PARTICLE_EXPLOSION_CLIP1);
        _particleExplosionClip2 = Resources.Load<AudioClip>(PARTICLE_EXPLOSION_CLIP2);
        _startGameClip = Resources.Load<AudioClip>(START_GAME_CLIP);
        _resumeGameClip = Resources.Load<AudioClip>(RESUME_GAME_CLIP);
        _pauseGameClip = Resources.Load<AudioClip>(PAUSE_GAME_CLIP);
        _overGameClip = Resources.Load<AudioClip>(OVER_GAME_CLIP);
        _ballClip1 = Resources.Load<AudioClip>(BALL_CLIP1);
    }

    public void PlayTouch1()
    {
        _audioSource.PlayOneShot(_ballClip1);
    }
    public void PlayExplosion1()
    {
        _audioSource.PlayOneShot(_particleExplosionClip1);
    }
    public void PlayExplosion2()
    {
        _audioSource.PlayOneShot(_particleExplosionClip2);
        _mobileVibration.Vibrate();
    }
    public void PlayGameOver()
    {
        _audioSource.PlayOneShot(_overGameClip);
    }
    public void PlayPause()
    {
        _audioSource.PlayOneShot(_pauseGameClip);
    }
    public void PlayResume()
    {
        _audioSource.PlayOneShot(_resumeGameClip);
    }
    public void PlayStart()
    {
        _audioSource.PlayOneShot(_startGameClip);
    }




}

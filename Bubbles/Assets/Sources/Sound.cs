using UnityEngine;
using UnityEngine.Audio;

public class Sound
{
    private AudioSource _audioSource;
    private AudioMixerGroup _masterGroup;
    private MobileVibration _mobileVibration = new MobileVibration();
    private AudioClip _particleExplosionClip1;
    private AudioClip _particleExplosionClip2;
    private AudioClip _startGameClip;
    private AudioClip _resumeGameClip;
    private AudioClip _pauseGameClip;
    private AudioClip _overGameClip;
    private AudioClip _ballClip1;
    private AudioClip _ballClip2;

    public Sound(AudioSource audioSource, AudioMixerGroup masterGroup)
    {
        _audioSource = audioSource;
        _masterGroup = masterGroup;
        _audioSource.outputAudioMixerGroup = masterGroup;
        _particleExplosionClip1 = Resources.Load<AudioClip>("AudioClips/ParticleExplosionClip1");
        _particleExplosionClip2 = Resources.Load<AudioClip>("AudioClips/ParticleExplosionClip2");
        _startGameClip = Resources.Load<AudioClip>("AudioClips/StartGameClip");
        _resumeGameClip = Resources.Load<AudioClip>("AudioClips/ResumeGameClip");
        _pauseGameClip = Resources.Load<AudioClip>("AudioClips/PauseGameClip");
        _overGameClip = Resources.Load<AudioClip>("AudioClips/OverGameClip;");
        _ballClip1 = Resources.Load<AudioClip>("AudioClips/BallClip1");
        _ballClip2 = Resources.Load<AudioClip>("AudioClips/BallClip2");
    }

    public void PlayTouch1()
    {
        _audioSource.PlayOneShot(_ballClip1);
    }
    public void PlayTouch2()
    {
        _audioSource.PlayOneShot(_ballClip2);
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

using UnityEngine;
using UnityEngine.Audio;

public class Game : MonoBehaviour
{
    [SerializeField] private UI _userInterface;
    [SerializeField] private Pool _pool;
    [SerializeField] private GameOverZone _gameOverZone;
    [SerializeField] private AnimationCurve _difficultyCurve;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioMixerGroup _masterGroup;
    [SerializeField] private AudioMixerSnapshot _snapshotInGame;
    [SerializeField] private AudioMixerSnapshot _snapshotInMenu;
    [SerializeField] private float _intervalLaunchComet;
    [SerializeField] private float _healthPlayer;
    private Timer _timer;
    private Player _player;
    private GameState _gameState;
    private PlayerInput _playerInput;
    private Difficulty _difficulty;
    private Sound _sound;
    private bool _isPaused = true;
    private bool _isAlive = true;
    private void Start()
    {
        _playerInput = new PlayerInput();
        _sound = new Sound(_audioSource, _masterGroup);
        _gameState = new GameState(_userInterface, _playerInput, _sound);
        _timer = new Timer(_intervalLaunchComet);
        _player = new Player(_healthPlayer);
        _difficulty = new Difficulty(_difficultyCurve);
        _pool.Init(_difficulty, _sound);
        SubscribeEvents();
        _sound.PlayPause();
    }
    private void SubscribeEvents()
    {
        _timer.OnWakeUp += LaunchComet;

        _gameState.OnPause += PauseGame;
        _gameState.OnStart += StartGame;
        _gameState.OnRestart += RestartGame;

        _playerInput.OnCometDetected += _pool.DetectComet;
        _playerInput.OnCometDetected += _player.SetScore;

        _gameOverZone.OnGiveDamage += _player.TakeDamage;
        _gameOverZone.OnDeadZoneTriggered += _pool.FallenComet;

        _player.OnChangedScore += _userInterface.SetScore;
        _player.OnChangedScore += _difficulty.ChangeDifficulty;
        _player.OnChangedHealth += _userInterface.SetHealth;
        _player.OnPlayerDeadScore += _gameState.GameOver;
        _player.OnPlayerDead += OverGame;
    }
    private void Update()
    {
        if (_isPaused == false)
        {
            _pool.Mover.MoveUpdate();
            if (_isAlive == true)
            {
                _timer.TimerUpdate(Time.deltaTime);
            }
        }
    }
    private void OnApplicationPause(bool pause)
    {
        //PauseGame();
    }
    private void OnApplicationQuit()
    {
        //ExitGame();
    }
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        _timer.OnWakeUp -= LaunchComet;
        _gameState.OnPause -= PauseGame;
        _gameState.OnStart -= StartGame;
        _gameState.OnRestart -= RestartGame;

        _playerInput.OnCometDetected -= _pool.DetectComet;
        _playerInput.OnCometDetected -= _player.SetScore;

        _gameOverZone.OnGiveDamage -= _player.TakeDamage;
        _gameOverZone.OnDeadZoneTriggered -= _pool.FallenComet;

        _player.OnChangedScore -= _userInterface.SetScore;
        _player.OnChangedScore -= _difficulty.ChangeDifficulty;
        _player.OnChangedHealth -= _userInterface.SetHealth;
        _player.OnPlayerDeadScore -= _gameState.GameOver;
        _player.OnPlayerDead -= OverGame;
    }
    private void OverGame()
    {
        _isAlive = false;
        _gameOverZone.gameObject.SetActive(false);
    }
    private void LaunchComet()
    {
        _pool.LaunchComet();
    }
    private void PauseGame(bool pause)
    {
        _isPaused = pause;
    }
    private void StartGame()
    {
        _isPaused = false;
        _isAlive = true;
        _userInterface.SetScore(0);
        _userInterface.SetMaxHealth(_healthPlayer);
        LaunchComet();
    }
    private void RestartGame()
    {
        _pool.ResetPool();
        _player.ResetPlayer(_healthPlayer);
        _timer.ResetTimer();
        _gameOverZone.gameObject.SetActive(true);
        StartGame();
    }
}

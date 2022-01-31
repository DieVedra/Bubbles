using UnityEngine;

public class Game : MonoBehaviour
{
    private const int START_SCORE = 0;
    [SerializeField] private UI _userInterface;
    [SerializeField] private Pool _pool;
    [SerializeField] private GameOverZone _gameOverZone;
    [SerializeField] private AnimationCurve _difficultyCurve;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _intervalLaunchBall;
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
        _sound = new Sound(_audioSource);
        _sound.PlayPause();
        _playerInput = new PlayerInput();
        _gameState = new GameState(_userInterface, _playerInput, _sound);
        _timer = new Timer(_intervalLaunchBall);
        _player = new Player(_healthPlayer);
        _difficulty = new Difficulty(_difficultyCurve);
        _pool.Init(_difficulty, _sound);
        SubscribeEvents();
    }
    private void SubscribeEvents()
    {
        _timer.OnWakeUp += _pool.LaunchBall;
        _gameState.OnPause += PauseGame;
        _gameState.OnStart += StartGame;
        _gameState.OnRestart += RestartGame;

        _playerInput.OnBallDetected += _pool.DetectBall;
        _playerInput.OnBallDetected += _player.SetScore;

        _gameOverZone.OnGiveDamage += _player.TakeDamage;
        _gameOverZone.OnDeadZoneTriggered += _pool.FallenBall;

        _player.OnChangedScore += _userInterface.SetScore;
        _player.OnChangedScore += _difficulty.ChangeDifficulty;
        _player.OnChangedHealth += _userInterface.SetHealth;
        _player.OnPlayerDeadScore += _gameState.GameOver;
        _player.OnPlayerDead += OverGame;

        _difficulty.OnChangeDifficulty += _gameOverZone.SetDifficulty;
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
    private void OnDisable()
    {
        _timer.OnWakeUp -= _pool.LaunchBall;
        _gameState.OnPause -= PauseGame;
        _gameState.OnStart -= StartGame;
        _gameState.OnRestart -= RestartGame;

        _playerInput.OnBallDetected -= _pool.DetectBall;
        _playerInput.OnBallDetected -= _player.SetScore;

        _gameOverZone.OnGiveDamage -= _player.TakeDamage;
        _gameOverZone.OnDeadZoneTriggered -= _pool.FallenBall;

        _player.OnChangedScore -= _userInterface.SetScore;
        _player.OnChangedScore -= _difficulty.ChangeDifficulty;
        _player.OnChangedHealth -= _userInterface.SetHealth;
        _player.OnPlayerDeadScore -= _gameState.GameOver;
        _player.OnPlayerDead -= OverGame;

        _difficulty.OnChangeDifficulty -= _gameOverZone.SetDifficulty;
    }
    private void OverGame()
    {
        _isAlive = false;
        _gameOverZone.gameObject.SetActive(false);
    }
    private void PauseGame(bool pause)
    {
        _isPaused = pause;
    }
    private void StartGame()
    {
        _isPaused = false;
        _isAlive = true;
        _userInterface.SetScore(START_SCORE);
        _userInterface.SetMaxHealth(_healthPlayer);
        _pool.LaunchBall();
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

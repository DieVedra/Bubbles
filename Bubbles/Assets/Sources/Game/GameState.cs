using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameState
{
    private const int TIME_DELAY = 2500;
    private UI _userInterface;
    private PlayerInput _playerInput;
    private Sound _sound;
    public event Action OnStart;
    public event Action<bool> OnPause;
    public event Action OnRestart;
    public event Action OnGameOver;
    public GameState(UI userInterface, PlayerInput playerInput, Sound sound)
    {
        _userInterface = userInterface;
        _sound = sound;
        _userInterface.ActivatePanelStartMenu();
        SubscribeStartButton();
        _playerInput = playerInput;
    }
    public void StartGame()
    {
        _userInterface.ActivatePanelGameBar();
        _playerInput.TouchEnable();
        _sound.PlayStart();
        SubscribePauseButton();
        OnStart?.Invoke();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        _userInterface.ActivatePanelGameBar();
        _playerInput.TouchEnable();
        _sound.PlayResume();
        SubscribePauseButton();
        OnPause?.Invoke(false);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        _userInterface.ActivatePanelGameBar();
        _playerInput.TouchEnable();
        _sound.PlayStart();
        SubscribePauseButton();
        OnRestart?.Invoke();
    }
    public void PauseGame()
    {
        _userInterface.ActivatePanelPauseMenu();
        _playerInput.TouchDisable();
        _sound.PlayPause();
        SubscribeResumeButton();
        SubscribeRestartButton();
        SubscribeExitButton();
        OnPause?.Invoke(true);
        Time.timeScale = 0f;
    }
    public async void GameOver(int score)
    {
        _playerInput.TouchDisable();
        SubscribeRestartButton();
        SubscribeExitButton();
        _userInterface.DisableGameBar();
        _sound.PlayGameOver();

        await Task.Delay(TIME_DELAY);

        _userInterface.ActivatePanelDeadMenu(score);
        OnPause?.Invoke(true);
        Time.timeScale = 0f;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    private void SubscribePauseButton()
    {
        _userInterface.PanelGameBar.ButtonPauseGame.onClick.AddListener(PauseGame);
    }
    private void SubscribeStartButton()
    {
        _userInterface.PanelStartMenu.ButtonStartGame.onClick.AddListener(StartGame);
    }
    private void SubscribeResumeButton()
    {
        _userInterface.PanelPauseMenu.ButtonResumeGame.onClick.AddListener(ResumeGame);
    }
    private void SubscribeRestartButton()
    {
        _userInterface.PanelPauseMenu.ButtonRestartGame.onClick.AddListener(RestartGame);
    }
    private void SubscribeExitButton()
    {
        _userInterface.PanelPauseMenu.ButtonExitGame.onClick.AddListener(ExitGame);
    }
}

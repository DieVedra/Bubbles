using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelPauseMenu : MonoBehaviour
{
    private const string FINAL_SCORE_TEXT = "Final Score: "; 
    [SerializeField] private Button _buttonResumeGame;
    [SerializeField] private Button _buttonRestartGame;
    [SerializeField] private Button _buttonExitGame;
    [SerializeField] private TextMeshProUGUI _scoreText;
    public Button ButtonResumeGame => _buttonResumeGame;
    public Button ButtonRestartGame => _buttonRestartGame;
    public Button ButtonExitGame => _buttonExitGame;
    
    public void ViewFinalScore(int score)
    {
        _scoreText.text = $"{FINAL_SCORE_TEXT}{score}";
    }
    public void EnableDeadMode()
    {
        _buttonResumeGame.gameObject.SetActive(false);
        _scoreText.gameObject.SetActive(true);
    }
    public void DisableDeadMode()
    {
        _buttonResumeGame.gameObject.SetActive(true);
        _scoreText.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        _buttonExitGame.onClick.RemoveAllListeners();
        _buttonRestartGame.onClick.RemoveAllListeners();
        _buttonResumeGame.onClick.RemoveAllListeners();
    }
}

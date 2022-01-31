using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelGameBar : MonoBehaviour
{
    [SerializeField] private Button _buttonPauseGame;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Slider _healthSlider;
    public Button ButtonPauseGame => _buttonPauseGame;
    public void SetHealth(float value)
    {
        if (value >= 0f)
        {
            _healthSlider.value = value;
        }
    }
    public void SetMaxHealth(float value)
    {
        _healthSlider.maxValue = value;
        SetHealth(value);
    }
    public void SetScore(int value)
    {
        _scoreText.text = $": {value} ";
    }
    private void OnDisable()
    {
        _buttonPauseGame.onClick.RemoveAllListeners();
    }
}

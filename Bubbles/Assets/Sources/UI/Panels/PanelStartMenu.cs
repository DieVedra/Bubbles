using UnityEngine;
using UnityEngine.UI;

public class PanelStartMenu : MonoBehaviour
{
    [SerializeField] private Button _buttonStartGame;
    public Button ButtonStartGame => _buttonStartGame;
    private void OnDisable()
    {
        _buttonStartGame.onClick.RemoveAllListeners();
    }
}

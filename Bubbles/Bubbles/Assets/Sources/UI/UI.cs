using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private PanelStartMenu _panelStartMenu;
    [SerializeField] private PanelPauseMenu _panelPauseMenu;
    [SerializeField] private PanelGameBar _panelGameBar;
    public PanelStartMenu PanelStartMenu => _panelStartMenu;
    public PanelPauseMenu PanelPauseMenu => _panelPauseMenu;
    public PanelGameBar PanelGameBar => _panelGameBar;
    public void ActivatePanelStartMenu()
    {
        _panelStartMenu.gameObject.SetActive(true);
    }
    public void ActivatePanelGameBar()
    {
        _panelGameBar.gameObject.SetActive(true);
        _panelPauseMenu.gameObject.SetActive(false);
        _panelStartMenu.gameObject.SetActive(false);
    }
    public void ActivatePanelPauseMenu()
    {
        _panelPauseMenu.gameObject.SetActive(true);
        _panelPauseMenu.DisableDeadMode();
        _panelGameBar.gameObject.SetActive(false);
    }
    public void ActivatePanelDeadMenu(int score)
    {
        _panelPauseMenu.gameObject.SetActive(true);
        _panelPauseMenu.EnableDeadMode();
        _panelPauseMenu.ViewFinalScore(score);
    }
    public void DisableGameBar()
    {
        _panelGameBar.gameObject.SetActive(false);
    }
    public void SetScore(int value)
    {
        _panelGameBar.SetScore(value);
    }
    public void SetHealth(float value)
    {
        _panelGameBar.SetHealth(value);
    }
    public void SetMaxHealth(float value)
    {
        _panelGameBar.SetMaxHealth(value);
    }
}

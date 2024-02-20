using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static Action OnGameReset;
    public static Action OnGameStart;
    
    [SerializeField] private GameObject menuPanel,gamePanel,gameOverPanel;
    private void OnEnable() {
        GameManager.OnGameStateChanged += UIUpdate;
    }
    private void OnDisable() {
        GameManager.OnGameStateChanged -= UIUpdate;
    }
    private void Start() {
        GameManager.OnGameStateChanged?.Invoke(GameState.Menu);
    }
    private void UIUpdate( GameState state) {
        switch (state)
        {
            case GameState.Menu:
                menuPanel.SetActive(CloseAllPanelExceptThis());
                break;
            case GameState.InGame:
                gamePanel.SetActive(CloseAllPanelExceptThis());
                break;
            case GameState.GameOver:
                gameOverPanel.SetActive(CloseAllPanelExceptThis());
                break;
        }
    }
    private bool CloseAllPanelExceptThis() {
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        return true;
    }
    public void StartButton() {
        GameManager.OnGameStateChanged?.Invoke(GameState.InGame);
        OnGameStart?.Invoke();
    }
    public void MenuButton() {
        GameManager.OnGameStateChanged?.Invoke(GameState.Menu);
        OnGameReset?.Invoke();
    }
    public void ExitButton() {
        Application.Quit();
    }
}

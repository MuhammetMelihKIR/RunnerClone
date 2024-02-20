using System;
using UnityEngine;
public enum GameState {
    Menu,InGame,GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Action<GameState> OnGameStateChanged;
    
    private GameState _gameState;

    private void Awake() {
        Instance = this;
    }
    private void OnEnable() {
        OnGameStateChanged += SetGameState;
    }
    private void OnDisable() {
        OnGameStateChanged -= SetGameState;
    }
    private void SetGameState(GameState gameState) {
        _gameState = gameState;
    }
    public bool IsGameState(GameState gameState) {
        return _gameState == gameState;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.Battle);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Battle:
                Debug.Log("The state is " + State);
                break;
            case GameState.Draft:
                Debug.Log("The state is " + State);
                break;
            case GameState.Win:
                Debug.Log("The state is " + State);
                break;
            case GameState.Loss:
                Debug.Log("The state is " + State);
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }
}

public enum GameState
{
    Battle,
    Draft,
    Win,
    Loss
}
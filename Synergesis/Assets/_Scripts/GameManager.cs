using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    public GameObject FadeScreen;
    public GameObject DeckButton;

    [SerializeField] float fadeLength = 0.5f;


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
                FadeIn(FadeScreen);
                HandleBattleState();
                break;

            case GameState.Draft:
                Debug.Log("The state is " + State);
                FadePartial(FadeScreen, 0.85f);
                HandleDraftState();
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

    public void FadeIn(GameObject gameObject)
    {
        Image screen = gameObject.GetComponent<Image>();
        screen.DOFade(0, fadeLength);
    }

    public void FadePartial(GameObject gameObject, float percent)
    {
        Image screen = gameObject.GetComponent<Image>();
        screen.DOFade(percent, fadeLength);
    }

    public void FadeOut(GameObject gameObject)
    {
        Image screen = gameObject.GetComponent<Image>();
        screen.DOFade(1f, fadeLength);
    }

    public void HandleBattleState()
    {
        DeckButton.SetActive(true);
    }

    public void HandleDraftState()
    {
        DeckButton.SetActive(false);
    }

}

public enum GameState
{
    Battle,
    Draft,
    Win,
    Loss
}
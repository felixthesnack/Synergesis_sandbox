using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Battle,
    Skill,
    Draft,
    Win,
    Loss
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;
    public GameState previousState;


    public static event Action<GameState> OnGameStateChanged;

    public GameObject FadeScreen;
    public GameObject DeckButton;
    [SerializeField] GameObject deckCanvas;
    [SerializeField] GameObject skillCanvas;

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
        previousState = State;
        State = newState;

        switch (newState)
        {
            case GameState.Battle:
                Debug.Log("The state is " + State);
                HandleBattleState();
                break;

            case GameState.Skill:
                Debug.Log("The state is " + State);
                HandleSkillState();
                break;

            case GameState.Draft:
                Debug.Log("The state is " + State);
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


    public void HandleBattleState()
    {
        DeckButton.SetActive(true);
        deckCanvas.gameObject.SetActive(false);
        CountersUI.Instance.UpdateTurn();
        FadeIn(FadeScreen);
    }
    public void HandleSkillState()
    {
        FadePartial(FadeScreen, 0.85f);
    }

    public void HandleDraftState()
    {
        skillCanvas.gameObject.SetActive(false);
        if(previousState == GameState.Battle)
        {
            FadePartial(FadeScreen, 0.85f);
        }
        DeckButton.SetActive(false);
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

}
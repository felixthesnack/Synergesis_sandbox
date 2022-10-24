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

    [SerializeField] PlayerDeck playerDeck;
    [SerializeField] GameObject HandParent;
    [SerializeField] GameObject FadeScreen;
    [SerializeField] float fadeLength = 0.5f;


    //[SerializeField] SlotManager slotManager;


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
                FadeOut(FadeScreen);
                ResetState();
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
    public void ResetState()
    {
        Utilities.DeleteChildren(HandParent.transform);

        playerDeck.priority0count = 0;
        playerDeck.priority1count = 0;
        playerDeck.priority2count = 0;
        playerDeck.priority3count = 0;
        playerDeck.priority4count = 0;

        playerDeck.cardsInHandCount = 0;
        playerDeck.cardsDrawn = 0;
        playerDeck.cardsPlayed = 0;

        //StartCoroutine(playerDeck.StartTurn());

        print("Player Reset");
    }

    public void FadeIn(GameObject gameObject)
    {
        Image screen = gameObject.GetComponent<Image>();
        screen.DOFade(1, fadeLength);
    }

    public void FadePartial(GameObject gameObject)
    {
        Image screen = gameObject.GetComponent<Image>();
        screen.DOFade(0.85f, fadeLength);
    }

    public void FadeOut(GameObject gameObject)
    {
        Image screen = gameObject.GetComponent<Image>();
        screen.DOFade(0f, fadeLength);
    }

}

public enum GameState
{
    Battle,
    Draft,
    Win,
    Loss
}
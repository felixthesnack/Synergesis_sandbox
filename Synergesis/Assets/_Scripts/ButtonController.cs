using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject HandParent;
    [SerializeField] PlayerDeck playerDeck;
    [SerializeField] Camera mainCam;
    [SerializeField] Canvas deckCanvas;
    [SerializeField] DeckUI deckUI;
    [SerializeField] SlotManager slotManager;

    [SerializeField] GameObject SortPriorityUp;
    [SerializeField] GameObject SortPriorityDown;
    [SerializeField] GameObject SortPriorityOff;

    [SerializeField] GameObject SortColorUp;
    [SerializeField] GameObject SortColorDown;
    [SerializeField] GameObject SortColorOff;

    [SerializeField] GameObject SortDrawsUp;
    [SerializeField] GameObject SortDrawsDown;
    [SerializeField] GameObject SortDrawsOff;

    [SerializeField] GameObject SortGoldUp;
    [SerializeField] GameObject SortGoldDown;
    [SerializeField] GameObject SortGoldOff;

    [SerializeField] GameObject SortManaUp;
    [SerializeField] GameObject SortManaDown;
    [SerializeField] GameObject SortManaOff;

    public void ResetScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Utilities.DeleteChildren(HandParent.transform);

        //playerDeck.deck = playerDeck.starterDeck;

        playerDeck.priority0count = 0;
        playerDeck.priority1count = 0;
        playerDeck.priority2count = 0;
        playerDeck.priority3count = 0;
        playerDeck.priority4count = 0;

        playerDeck.cardsInHandCount = 0;
        playerDeck.cardsDrawn = 0;
        playerDeck.cardsPlayed = 0;


        StartCoroutine(playerDeck.StartTurn());

        print("Game Reset");
    }

    public void AddCard()
    {
        int r = Random.Range(0,6);
        playerDeck.deck.Insert(0, CardDatabase.cardList[r]);
        //playerDeck.staticDeck.Add(playerDeck.deck[r]);
        playerDeck.deckSize = playerDeck.deck.Count;
        slotManager.LoadSynergyBar();
    }

    public void ViewDeck()
    {
        //deckUI.LoadStaticDeck();
        deckCanvas.gameObject.SetActive(true);
        mainCam.transform.DOMove(new Vector3(0, -1080, -10),1.2f).SetEase(Ease.InOutSine);
    }

    public void SortPriorityAcending()
    {
        SortPriorityUp.SetActive(false);
        SortPriorityDown.SetActive(true);
        SortPriorityOff.SetActive(false);

        DeckUI.SortDeck -= deckUI.SortByPriorityDescending;
        DeckUI.SortDeck += deckUI.SortByPriorityAcending;
        DeckUI.SortDeck?.Invoke();
    }

    public void SortPriorityDescending()
    {
        SortPriorityUp.SetActive(true);
        SortPriorityDown.SetActive(false);
        SortPriorityOff.SetActive(false);

        DeckUI.SortDeck -= deckUI.SortByPriorityAcending;
        DeckUI.SortDeck += deckUI.SortByPriorityDescending;
        DeckUI.SortDeck?.Invoke();
    }

    public void SortPriority()
    {
        SortPriorityUp.SetActive(false);
        SortPriorityDown.SetActive(false);
        SortPriorityOff.SetActive(true);

        DeckUI.SortDeck -= deckUI.SortByPriorityAcending;
        DeckUI.SortDeck -= deckUI.SortByPriorityDescending;
    }

    public void SortColorAcending()
    {
        SortColorUp.SetActive(false);
        SortColorDown.SetActive(true);
        SortColorOff.SetActive(false);

        DeckUI.SortDeck -= deckUI.SortByColorDescending;
        DeckUI.SortDeck += deckUI.SortByColorAcending;
        DeckUI.SortDeck?.Invoke();
    }

    public void SortColorDescending()
    {
        SortColorUp.SetActive(true);
        SortColorDown.SetActive(false);
        SortColorOff.SetActive(false);

        DeckUI.SortDeck -= deckUI.SortByColorAcending;
        DeckUI.SortDeck += deckUI.SortByColorDescending;
        DeckUI.SortDeck?.Invoke();
    }

    public void SortColor()
    {
        SortColorUp.SetActive(false);
        SortColorDown.SetActive(false);
        SortColorOff.SetActive(true);

        DeckUI.SortDeck -= deckUI.SortByColorAcending;
        DeckUI.SortDeck -= deckUI.SortByColorDescending;
    }

    public void SortDrawsAcending()
    {
        SortDrawsUp.SetActive(false);
        SortDrawsDown.SetActive(true);
        SortDrawsOff.SetActive(false);

        DeckUI.SortDeck -= deckUI.SortByDrawsDescending;
        DeckUI.SortDeck += deckUI.SortByDrawsAcending;
        DeckUI.SortDeck?.Invoke();
    }

    public void SortDrawsDescending()
    {
        SortDrawsUp.SetActive(true);
        SortDrawsDown.SetActive(false);
        SortDrawsOff.SetActive(false);

        DeckUI.SortDeck -= deckUI.SortByDrawsAcending;
        DeckUI.SortDeck += deckUI.SortByDrawsDescending;
        DeckUI.SortDeck?.Invoke();
    }

    public void SortDraws()
    {
        SortDrawsUp.SetActive(false);
        SortDrawsDown.SetActive(false);
        SortDrawsOff.SetActive(true);

        DeckUI.SortDeck -= deckUI.SortByDrawsAcending;
        DeckUI.SortDeck -= deckUI.SortByDrawsDescending;
    }

    public void SortGoldAcending()
    {
        SortGoldUp.SetActive(false);
        SortGoldDown.SetActive(true);
        SortGoldOff.SetActive(false);

        DeckUI.SortDeck -= deckUI.SortByGoldDescending;
        DeckUI.SortDeck += deckUI.SortByGoldAcending;
        DeckUI.SortDeck?.Invoke();
    }

    public void SortGoldDescending()
    {
        SortGoldUp.SetActive(true);
        SortGoldDown.SetActive(false);
        SortGoldOff.SetActive(false);

        DeckUI.SortDeck -= deckUI.SortByGoldAcending;
        DeckUI.SortDeck += deckUI.SortByGoldDescending;
        DeckUI.SortDeck?.Invoke();
    }

    public void SortGold()
    {
        SortGoldUp.SetActive(false);
        SortGoldDown.SetActive(false);
        SortGoldOff.SetActive(true);

        DeckUI.SortDeck -= deckUI.SortByGoldAcending;
        DeckUI.SortDeck -= deckUI.SortByGoldDescending;
    }
    public void SortManaAcending()
    {
        SortManaUp.SetActive(false);
        SortManaDown.SetActive(true);
        SortManaOff.SetActive(false);

        DeckUI.SortDeck -= deckUI.SortByManaDescending;
        DeckUI.SortDeck += deckUI.SortByManaAcending;
        DeckUI.SortDeck?.Invoke();
    }

    public void SortManaDescending()
    {
        SortManaUp.SetActive(true);
        SortManaDown.SetActive(false);
        SortManaOff.SetActive(false);

        DeckUI.SortDeck -= deckUI.SortByManaAcending;
        DeckUI.SortDeck += deckUI.SortByManaDescending;
        DeckUI.SortDeck?.Invoke();
    }

    public void SortMana()
    {
        SortManaUp.SetActive(false);
        SortManaDown.SetActive(false);
        SortManaOff.SetActive(true);

        DeckUI.SortDeck -= deckUI.SortByManaAcending;
        DeckUI.SortDeck -= deckUI.SortByManaDescending;
    }

    public void Back()
    {
        mainCam.transform.DOMove(new Vector3(0, 0, -10), 1.2f).SetEase(Ease.InOutSine).OnComplete(DisableCan);
        void DisableCan () 
        {
            deckCanvas.gameObject.SetActive(false);
        }
    }
}


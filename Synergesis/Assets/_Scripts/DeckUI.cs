using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using TMPro;

public class DeckUI : MonoBehaviour
{
    public static DeckUI Instance;

    public PlayerDeck playerDeck;
    public CardUI cardUI;
    public List<Card> deckToSort;
    public List<Card> deckToCount = new List<Card>();

    private int cardCount = 0;

    public GameObject DeckUIContainer;

    public GameObject CardSlotContainer;
    public GameObject CardSlot;

    public GameObject ScrollBarUI;

    public GameObject ZoomPanel;


    public static Action SortDeck;


    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        //deckToSort = playerDeck.deck.Distinct().ToList();
        LoadStaticDeck();
        SortDeck?.Invoke();
        LoadSortedDeckUI();
    }



    public void LoadSortedDeckUI()
    {        

        int sortedDeckCount = deckToSort.Count;
        for (int i = 0; i < sortedDeckCount; i++)
        {
            GameObject index = DeckUIContainer.transform.GetChild(i).transform.GetChild(0).gameObject;
            DeckCardUI cardSlotUI = index.GetComponent<DeckCardUI>();

            cardSlotUI.LoadCard(deckToSort[i]);

        }
    }

    public void LoadStaticDeck()
    {
        deckToSort = playerDeck.deck.Distinct().ToList();

        int cardSlotCount = DeckUIContainer.transform.childCount;
        int deckCount = deckToSort.Count;

        deckToCount.AddRange(playerDeck.deck);


        for (int i = 0; i < (deckCount - cardSlotCount); i++)
        {
            GameObject cardSlotContainer = Instantiate(CardSlotContainer, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation, DeckUIContainer.transform) as GameObject;
            GameObject cardSlot = Instantiate(CardSlot, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation, cardSlotContainer.transform) as GameObject;
            cardSlot.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
            //cardSlot.transform.SetParent(cardSlotContainer.transform);
            //cardSlot.transform.position = new Vector3(0f, 0f, 0f);
        }

        for (int i = 0; i < deckCount; i++)
        {
            while (deckToCount.Contains(deckToSort[i]))
            {
                int id = deckToSort[i].id;
                int xIndex = deckToCount.FindIndex(x => x.id == id);
                deckToCount.Remove(deckToCount[xIndex]);

                cardCount++;
            }

            deckToSort[i].counter = cardCount;
            cardCount = 0;
        }

    }

    public void SortByPriorityDescending()
    {
        List<Card> sortedDeck = deckToSort.OrderByDescending(card => card.priority).ToList();
        deckToSort = sortedDeck;
    }

    public void SortByPriorityAcending()
    {
        List<Card> sortedDeck = deckToSort.OrderBy(card => card.priority).ToList();
        deckToSort = sortedDeck;
    }

    public void SortByColorDescending()
    {
        List<Card> sortedDeck = deckToSort.OrderByDescending(card => card.color).ToList();
        deckToSort = sortedDeck;
    }

    public void SortByColorAcending()
    {
        List<Card> sortedDeck = deckToSort.OrderBy(card => card.color).ToList();
        deckToSort = sortedDeck;
    }
    public void SortByDrawsDescending()
    {
        List<Card> sortedDeck = deckToSort.OrderByDescending(card => card.draws).ToList();
        deckToSort = sortedDeck;
    }

    public void SortByDrawsAcending()
    {
        List<Card> sortedDeck = deckToSort.OrderBy(card => card.draws).ToList();
        deckToSort = sortedDeck;
    }

    public void SortByGoldDescending()
    {
        List<Card> sortedDeck = deckToSort.OrderByDescending(card => card.gold).ToList();
        deckToSort = sortedDeck;
    }

    public void SortByGoldAcending()
    {
        List<Card> sortedDeck = deckToSort.OrderBy(card => card.gold).ToList();
        deckToSort = sortedDeck;
    }

    public void SortByManaDescending()
    {
        List<Card> sortedDeck = deckToSort.OrderByDescending(card => card.mana).ToList();
        deckToSort = sortedDeck;
    }

    public void SortByManaAcending()
    {
        List<Card> sortedDeck = deckToSort.OrderBy(card => card.mana).ToList();
        deckToSort = sortedDeck;
    }
}
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class DeckUI : MonoBehaviour
{
    public PlayerDeck playerDeck;
    public CardUI cardUI;
    public List<Card> deckToSort;

    public GameObject DeckUIContainer;

    public GameObject CardSlot;

    public GameObject ScrollBarUI;

    public static Action SortDeck;

    private void OnEnable()
    {
        deckToSort = playerDeck.staticDeck;
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



    public void LoadSortedDeckUI()
    {
        int sortedDeckCount = deckToSort.Count;
        for (int i = 0; i < sortedDeckCount; i++)
        {
            GameObject index = DeckUIContainer.transform.GetChild(i).gameObject;
            CardUI cardSlotUI = index.GetComponent<CardUI>();

            cardSlotUI.LoadCard(deckToSort[i]);
        }
    }

    public void LoadStaticDeckUI()
    {
        int cardSlotCount = DeckUIContainer.transform.childCount;
        int staticDeckCount = playerDeck.staticDeck.Count;

        for (int i = 0; i < (staticDeckCount - cardSlotCount); i++)
        {
            GameObject cardSlot = Instantiate(CardSlot, transform.position, transform.rotation, DeckUIContainer.transform) as GameObject;
            cardSlot.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
            cardUI = cardSlot.GetComponent<CardUI>();

            cardUI.LoadCard(playerDeck.staticDeck[i]);
        }

        if(staticDeckCount > 12)
        {
            ScrollBarUI.SetActive(true);
        }
        else
        {
            ScrollBarUI.SetActive(false);
        }
    }

}

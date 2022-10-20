using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using TMPro;

public class DeckUI : MonoBehaviour
{
    public PlayerDeck playerDeck;
    public CardUI cardUI;
    public List<Card> deckToSort;
    public List<Card> deckToCount;

    private int cardCount = 0;

    public GameObject DeckUIContainer;

    public GameObject CardSlot;

    public GameObject ScrollBarUI;

    public GameObject CountText;
    public TMP_Text CountNumber;
    public GameObject MaxCount;

    public static Action SortDeck;


    private void OnEnable()
    {
        deckToSort = playerDeck.deck.Distinct().ToList();
        SortDeck?.Invoke();
        LoadStaticDeck();
        LoadSortedDeckUI();
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
        deckToCount.AddRange(playerDeck.deck);

        int sortedDeckCount = deckToSort.Count;
        for (int i = 0; i < sortedDeckCount; i++)
        {
            GameObject index = DeckUIContainer.transform.GetChild(i).gameObject;
            CardUI cardSlotUI = index.GetComponent<CardUI>();

            cardSlotUI.LoadCard(deckToSort[i]);

            while (deckToCount.Contains(deckToSort[i]))
            {
                int id = deckToSort[i].id;
                int xIndex = deckToCount.FindIndex(x => x.id == id);
                deckToCount.Remove(deckToCount[xIndex]);

                cardCount++;
            }

            CountText = index.transform.GetChild(3).gameObject;
            MaxCount = CountText.transform.GetChild(2).gameObject;

            if (cardCount > 1)
            {
                CountText.SetActive(true);
                CountNumber = CountText.transform.GetChild(1).GetComponent<TMP_Text>();
                CountNumber.text = cardCount.ToString();
                if (cardCount == 5)
                {
                    MaxCount.SetActive(true);
                }
                else
                {
                    MaxCount.SetActive(false);
                }
            }
            else
            {
                CountText.SetActive(false);
            }

            cardCount = 0;

        }
    }

    public void LoadStaticDeck()
    {
        int cardSlotCount = DeckUIContainer.transform.childCount;
        int deckCount = deckToSort.Count;

        for (int i = 0; i < (deckCount - cardSlotCount); i++)
        {
            GameObject cardSlot = Instantiate(CardSlot, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation, DeckUIContainer.transform) as GameObject;
            cardSlot.transform.localScale = new Vector3(1.2f, 1.2f, 1f);

        }

    }

}
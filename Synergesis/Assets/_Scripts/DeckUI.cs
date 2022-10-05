using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DeckUI : MonoBehaviour
{
    public PlayerDeck playerDeck;
    public CardUI cardUI;

    public GameObject DeckUIContainer;
    public GameObject SortPriorityUp;
    public GameObject SortPriorityDown;

    public GameObject CardSlot;

    public GameObject ScrollBarUI;


    public void SortByPriorityDescending()
    {
        SortPriorityUp.SetActive(true);
        SortPriorityDown.SetActive(false);
        
        List<Card> deckToSort = playerDeck.staticDeck;

        List<Card> sortedDeck = deckToSort.OrderByDescending(card => card.priority).ThenBy(card => card.color).ToList();
        
        LoadSortedDeckUI(sortedDeck);
    }

    public void SortByPriorityAcending()
    {
        SortPriorityUp.SetActive(false);
        SortPriorityDown.SetActive(true);
        List<Card> deckToSort = playerDeck.staticDeck;

        List<Card> sortedDeck = deckToSort.OrderBy(card => card.priority).ToList(); //.ThenBy(card => card.color).ToList();
        //int sortedDeckCount = sortedDeck.Count;

        //for (int i = 0; i < sortedDeckCount; i++)
        //{
        //    GameObject index = DeckUIContainer.transform.GetChild(i).gameObject;
        //    CardUI cardSlotUI = index.GetComponent<CardUI>();

        //    cardSlotUI.LoadCard(sortedDeck[i]);
        //}

        LoadSortedDeckUI(sortedDeck);
    }

    public void LoadSortedDeckUI(List<Card> sortedDeck)
    {
        int sortedDeckCount = sortedDeck.Count;
        for (int i = 0; i < sortedDeckCount; i++)
        {
            GameObject index = DeckUIContainer.transform.GetChild(i).gameObject;
            CardUI cardSlotUI = index.GetComponent<CardUI>();

            cardSlotUI.LoadCard(sortedDeck[i]);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckUI : MonoBehaviour
{
    public PlayerDeck playerDeck;
    public CardUI cardUI;

    public GameObject DeckUIContainer;

    public GameObject CardSlot;


    public void LoadStaticDeckUI()
    {
        for (int i = 0; i < playerDeck.staticDeck.Count; i++)
        {
            GameObject cardSlot = Instantiate(CardSlot, transform.position, transform.rotation, DeckUIContainer.transform) as GameObject;
            cardUI = cardSlot.GetComponent<CardUI>();

            cardUI.LoadCard(playerDeck.staticDeck[i]);
        }
    }

    public void AddStaticDeckUI()
    {
        if (playerDeck.staticDeck.Count < playerDeck.deckSize)
        {
            GameObject cardSlot = Instantiate(CardSlot, transform.position, transform.rotation, DeckUIContainer.transform) as GameObject;
            cardUI = cardSlot.GetComponent<CardUI>();

            cardUI.LoadCard(playerDeck.staticDeck[DeckUIContainer.transform.childCount - 1]);
        }
    }
}

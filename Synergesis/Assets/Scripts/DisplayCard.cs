using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayCard : MonoBehaviour
{
    public List<Card> displayCard = new List<Card>();
    //public int displayId;

    public int id;
    public string cardName;
    public int priority;
    public int gold;
    public int mana;
    public int draws;
    public string description;

    public Text nameText;
    public Text priorityText;
    public Text goldText;
    public Text manaText; 
    public Text drawsText; 
    public Text descriptionText;

    public int numberOfCardsInDeck;

    // Start is called before the first frame update
    void Start()
    {
        numberOfCardsInDeck = PlayerDeck.deckSize;

        //displayCard[0] = CardDatabase.cardList[displayId];
        
    }

    // Update is called once per frame
    void Update()
    {
        id = displayCard[0].id;
        cardName = displayCard[0].cardName;
        priority = displayCard[0].priority;
        gold = displayCard[0].gold;
        mana = displayCard[0].mana;
        draws = displayCard[0].draws;
        description = displayCard[0].description;

        nameText.text = " " + cardName;
        priorityText.text = " " + priority;
        goldText.text = " " + gold;
        manaText.text = " " + mana;
        drawsText.text = " " + draws;
        descriptionText.text = " " + description;

        if (this.tag == "Clone")
        {
            displayCard[0] = PlayerDeck.staticDeck[numberOfCardsInDeck - 1];
            numberOfCardsInDeck -= 1;
            PlayerDeck.deckSize -= 1;
            this.tag = "Untagged";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditCard : MonoBehaviour
{
    // Start is called before the first frame update

    public static void RaisePriority(Card card)
    {
        int cardId = card.id;
        int xIndex = PlayerDeck.Instance.deck.FindIndex(x => x.id == cardId);
        
        //PlayerDeck.Instance.deck.Remove(PlayerDeck.Instance.deck[xIndex]);

        string idString = card.id.ToString();
        char priorityDown = idString[(idString.Length - 5)];
        int output = priorityDown.CompareTo('0');

        Card tempCard = PlayerDeck.Instance.deck[xIndex];

        if (card.priority < 5)
        {
            if (output != 0)
            {
                //card.id -= 10000;
                tempCard.id -= 10000;

            }
            else
            {
                //card.id += 1000;
                tempCard.id += 1000;
            }
        }

        //card.priority += 1;
        tempCard.priority += 1;
        PlayerDeck.Instance.deck[xIndex] = tempCard;

        //cardId = card.id;
        //xIndex = CardDatabase.cardDatabase.FindIndex(x => x.id == cardId);
        //PlayerDeck.Instance.deck.Add(CardDatabase.cardDatabase[xIndex]);
        //PlayerDeck.Instance.deckSize = PlayerDeck.Instance.deck.Count;

    }
    public static void LowerPriority(Card card)
    {
        int cardId = card.id;
        int xIndex = PlayerDeck.Instance.deck.FindIndex(x => x.id == cardId);

        //PlayerDeck.Instance.deck.Remove(PlayerDeck.Instance.deck[xIndex]);

        string idString = card.id.ToString();
        char priorityUp = idString[(idString.Length - 4)];
        int output = priorityUp.CompareTo('0');

        Card tempCard = PlayerDeck.Instance.deck[xIndex];

        if (card.priority > 0)
        {
            if (output != 0)
            {
                //card.id -= 1000;
                tempCard.id -= 1000;
            }
            else
            {
                //card.id += 10000;
                tempCard.id += 10000;
            }
        }

        //card.priority -= 1;
        tempCard.priority -= 1;
        PlayerDeck.Instance.deck[xIndex] = tempCard;


        //cardId = card.id;
        //xIndex = CardDatabase.cardDatabase.FindIndex(x => x.id == cardId);
        //PlayerDeck.Instance.deck.Add(CardDatabase.cardDatabase[xIndex]);
        //PlayerDeck.Instance.deckSize = PlayerDeck.Instance.deck.Count;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();

    void Awake()
    {
        //starter deck
        cardList.Add(new Card(0, "White", "Starter", 1, 0, 1, 0, "Earn 1 mana", false));
        cardList.Add(new Card(1, "Gold", "Starter", 1, 0, 0, 1, "Earn 1 gold", false));
        cardList.Add(new Card(2, "Black", "Starter", 1, 1, 0, 0, "Draw 1 card", false));
        cardList.Add(new Card(3, "White", "Starter", 2, 0, 1, 0, "Earn 1 mana", false));
        cardList.Add(new Card(4, "Gold", "Starter", 2, 0, 0, 1, "Earn 1 gold", false));
        cardList.Add(new Card(5, "Black", "Starter", 2, 1, 0, 0, "Draw 1 card", false));

        //tier 1
        //basic
        cardList.Add(new Card(6, "White", "Basic", 1, 1, 2, 0, "Draw 1 card and earn 2 mana", false));
        cardList.Add(new Card(7, "White", "Basic", 2, 0, 2, 1, "Earn 2 mana and 1 gold", false));
        cardList.Add(new Card(8, "White", "Basic", 3, 0, 3, 0, "Earn 3 mana", false));
        cardList.Add(new Card(9, "Gold", "Basic", 1, 0, 1, 2, "Earn 2 gold and 1 mana", false));
        cardList.Add(new Card(10, "Gold", "Basic", 2, 1, 0, 2, "Draw 1 card and earn 2 gold", false));
        cardList.Add(new Card(11, "Gold", "Basic", 3, 0, 0, 3, "Earn 3 gold", false));
        cardList.Add(new Card(12, "Black", "Basic", 1, 1, 0, 2, "Draw 1 card and earn 2 gold", false));
        cardList.Add(new Card(13, "Black", "Basic", 2, 1, 2, 0, "Draw 1 card and earn 2 mana", false));
        cardList.Add(new Card(14, "Black", "Basic", 2, 2, 0, 0, "Draw 2 cards", false));

        //special
        cardList.Add(new Card(15, "White", "Ambitious", 3, 0, 3, 0, "Every time this card is played first according to its priority, lower it's priority by 1 and permanently add +1 mana", false));
        cardList.Add(new Card(16, "Gold", "Ambitious", 3, 0, 0, 3, "Every time this card is played first according to its priority, lower it's priority by 1 and permanently add +1 gold", false));
        cardList.Add(new Card(17, "Black", "Ambitious", 3, 1, 0, 0, "Every time this card is played first according to its priority, lower it's priority by 1 and permanently add +1 draw", false));
        cardList.Add(new Card(18, "White", "Jealous", 3, 0, 2, 0, "If the previous card played is white, draw 1 card", false));
        cardList.Add(new Card(19, "Gold", "Jealous", 3, 0, 1, 1, "If the previous card played is gold, draw 1 card", false));
        cardList.Add(new Card(20, "Black", "Jealous", 3, 1, 1, 0, "If the previous card played is black, draw 1 card", false));
        cardList.Add(new Card(21, "White", "Inspiring", 1, 0, 2, 0, "If the next card played is white, draw 1 card", false));
        cardList.Add(new Card(22, "Gold", "Inspiring", 1, 0, 1, 1, "If the next card played is gold, draw 1 card", false));
        cardList.Add(new Card(23, "Black", "Inspiring", 1, 1, 1, 0, "If the next card played is black, draw 1 card", false));
        cardList.Add(new Card(24, "White", "Skeptical", 2, 0, 2, 0, "If 10 cards with a lower priority than this one have been played this turn, draw 2 cards", false));
        cardList.Add(new Card(25, "Gold", "Skeptical", 2, 0, 0, 2, "If 10 cards with a lower priority than this one have been played this turn, draw 2 cards", false));
        cardList.Add(new Card(26, "Black", "Skeptical", 2, 0, 1, 1, "If 10 cards with a lower priority than this one have been played this turn, draw 2 cards", false));
        cardList.Add(new Card(27, "White", "Inquisitive", 1, 0, 2, 0, "Play one random white card from the deck with the same priority as this card (plays nothing if no options available)", false));
        cardList.Add(new Card(28, "Black", "Wise", 1, 0, 1, 1, "If your synergesis bar is 15 or higher when this card is played, draw a card", false));
        cardList.Add(new Card(29, "Colorless", "Medicinal", 1, 0, 0, 0, "Heal one point and trash this card (plays on draw, still gives one meter charge)", false));
        cardList.Add(new Card(30, "Gold", "Frugal", 3, 0, 0, 1, "Earn 1 gold for every card played after this one", false));
        
        //skills
        cardList.Add(new Card(31, "Black", "Motivating", 1, 1, 1, 0, "Lower one card's priority by 1 for next turn", true));
        cardList.Add(new Card(32, "Black", "Encouraging", 1, 1, 0, 1, "Raise one card's priority by 1 for next turn", true));
        cardList.Add(new Card(33, "White", "Mystical", 3, 0, 1, 1, "Spend 40 mana to add 2 cards to your starting hand next turn", true));
        cardList.Add(new Card(34, "Gold", "Extravagant", 3, 0, 0, 1, "Spend up to 120 gold to add extra cards to your starting hand in the next turn at 30 gold per draw", true));
        cardList.Add(new Card(35, "White", "Investigating", 2, 0, 2, 0, "Pick 1 card with a different priority than this card and put it into your starting hand on the next turn", true));
        cardList.Add(new Card(36, "White", "Speculative", 1, 0, 1, 1, "Add an extra random card to your starting hand on the next turn", true));
        cardList.Add(new Card(37, "Black", "Tidy", 3, 1, 0, 0, "Trash this card and another card from your deck and add 1 black starter card with priority 1", true));
        cardList.Add(new Card(38, "Colorless", "Neutral", 1, 0, 0, 0, "Skip the draft by not taking or trashing any cards from your deck, trash this card", true));
    }
}

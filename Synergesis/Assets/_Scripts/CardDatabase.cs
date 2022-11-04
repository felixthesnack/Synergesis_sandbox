using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static CardDatabase Instance;

    public static List<Card> cardList = new List<Card>();

    void Awake()
    {

        Instance = this;


        //starter deck
        cardList.Add(new Card(0, "White", "Starter", 1, 0, 1, 0, "Earn 1 mana", false, 0));
        cardList.Add(new Card(1, "Gold", "Starter", 1, 0, 0, 1, "Earn 1 gold", false, 0));
        cardList.Add(new Card(2, "Black", "Starter", 1, 1, 0, 0, "Draw 1 card", false, 0));
        cardList.Add(new Card(3, "White", "Starter", 2, 0, 1, 0, "Earn 1 mana", false, 0));
        cardList.Add(new Card(4, "Gold", "Starter", 2, 0, 0, 1, "Earn 1 gold", false, 0));
        cardList.Add(new Card(5, "Black", "Starter", 2, 1, 0, 0, "Draw 1 card", false, 0));

        //tier 1
        //basic
        cardList.Add(new Card(6, "White", "Summoning", 1, 1, 2, 0, "Draw 1 card and earn 2 mana", false, 0));
        cardList.Add(new Card(7, "Gold", "Summoning", 2, 1, 0, 2, "Draw 1 card and earn 2 gold", false, 0));
        cardList.Add(new Card(8, "Black", "Summoning", 2, 2, 0, 0, "Draw 2 card", false, 0));
        cardList.Add(new Card(9, "White", "Magical", 3, 0, 3, 0, "Earn 3 mana", false, 0));
        cardList.Add(new Card(10, "Gold", "Magical", 3, 0, 1, 2, "Earn 2 gold and 1 mana", false, 0));
        cardList.Add(new Card(11, "Black", "Magical", 2, 1, 2, 0, "Draw 1 card and earn 2 mana", false, 0));
        cardList.Add(new Card(12, "White", "Wealthy", 2, 0, 2, 1, "Earn 2 mana and 1 gold", false, 0));
        cardList.Add(new Card(13, "Gold", "Wealthy", 3, 0, 0, 3, "Earn 3 gold", false, 0));
        cardList.Add(new Card(14, "Black", "Wealthy", 1, 1, 0, 2, "Draw 1 card and earn 2 gold", false, 0));

        //special
        cardList.Add(new Card(15, "White", "Ambitious", 3, 0, 3, 0, "Every time this card is played first according to its priority, lower it's priority by 1 and permanently add +1 mana", false, 0));
        cardList.Add(new Card(16, "Gold", "Ambitious", 3, 0, 0, 3, "Every time this card is played first according to its priority, lower it's priority by 1 and permanently add +1 gold", false, 0));
        cardList.Add(new Card(17, "Black", "Ambitious", 3, 1, 0, 0, "Every time this card is played first according to its priority, lower it's priority by 1 and permanently add +1 draw", false, 0));
        cardList.Add(new Card(18, "White", "Jealous", 3, 0, 2, 0, "If the previous card played is white, draw 1 card", false, 0));
        cardList.Add(new Card(19, "Gold", "Jealous", 3, 0, 1, 1, "If the previous card played is gold, draw 1 card", false, 0));
        cardList.Add(new Card(20, "Black", "Jealous", 3, 1, 1, 0, "If the previous card played is black, draw 1 card", false, 0));
        cardList.Add(new Card(21, "White", "Inspiring", 1, 0, 2, 0, "If the next card played is white, draw 1 card", false, 0));
        cardList.Add(new Card(22, "Gold", "Inspiring", 1, 0, 1, 1, "If the next card played is gold, draw 1 card", false, 0));
        cardList.Add(new Card(23, "Black", "Inspiring", 1, 1, 1, 0, "If the next card played is black, draw 1 card", false, 0));
        cardList.Add(new Card(24, "White", "Skeptical", 2, 0, 2, 0, "If 10 card with a lower priority than this one have been played this turn, draw 2 card", false, 0));
        cardList.Add(new Card(25, "Gold", "Skeptical", 2, 0, 0, 2, "If 10 card with a lower priority than this one have been played this turn, draw 2 card", false, 0));
        cardList.Add(new Card(26, "Black", "Skeptical", 2, 0, 1, 1, "If 10 card with a lower priority than this one have been played this turn, draw 2 card", false, 0));
        cardList.Add(new Card(27, "White", "Inquisitive", 1, 0, 2, 0, "Play one random white card from the deck with the same priority as this card<br>Plays nothing if no options available", false, 0));
        cardList.Add(new Card(28, "Black", "Wise", 1, 1, 1, 0, "If your synergesis bar is 15 or higher when this card is played, draw a card", false, 0));
        cardList.Add(new Card(29, "Colorless", "Medicinal", 1, 0, 0, 0, "Heal 20hp and trash this card<br>Plays on draw<br>Still gives one meter charge<br>Counts as all colors", false, 0));
        cardList.Add(new Card(30, "Gold", "Frugal", 3, 0, 0, 1, "Earn 1 gold for every card played after this one", false, 0));

        //skills
        cardList.Add(new Card(31, "Black", "Motivating", 3, 1, 1, 0, "Lower one card priority by 1 for your next turn", true, 2));
        cardList.Add(new Card(32, "Black", "Encouraging", 3, 1, 0, 1, "Raise one card priority by 1 for your next turn", true, 2));
        cardList.Add(new Card(33, "White", "Mystical", 3, 0, 1, 1, "Spend 40 mana to add 2 card to your starting hand next turn", true, 3));
        cardList.Add(new Card(34, "Gold", "Extravagant", 3, 0, 0, 1, "Spend 30 gold to add an extra card to your starting hand up to 4 times", true, 3));
        cardList.Add(new Card(35, "White", "Investigating", 2, 0, 2, 0, "Pick 1 card with a different priority than this card and put it into your starting hand on the next turn<br>Does not increase starting hand size", true, 3));
        cardList.Add(new Card(36, "White", "Speculative", 3, 0, 1, 1, "Add 1 extra card to your starting hand on the next turn", true, 3));
        cardList.Add(new Card(37, "Black", "Tidy", 2, 1, 0, 0, "Trash this card and another card from your deck and add 1 black starter card with priority 1", true, 0));
        cardList.Add(new Card(38, "Gold", "Thrifty", 2, 0, 0, 1, "Recieve 5 coins, but you must trash a card this draft phase", true, 5));
        cardList.Add(new Card(39, "Colorless", "Neutral", 2, 0, 0, 0, "Skip the draft by not taking or trashing any card from your deck", true, 4));
    }
}

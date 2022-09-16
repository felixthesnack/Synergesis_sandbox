using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();

    void Awake()
    {
        //starter deck
        cardList.Add(new Card(0, "White", 0, 0, 1, 0, "Gives 1 Mana"));
        cardList.Add(new Card(1, "Black", 0, 0, 0, 1, "Draw Card"));
        cardList.Add(new Card(2, "Gold", 0, 1, 0, 0, "Gives 1 Gold"));
        cardList.Add(new Card(3, "White", 1, 0, 1, 0, "Gives 1 Mana"));
        cardList.Add(new Card(4, "Black", 1, 0, 0, 1, "Draw Card"));
        cardList.Add(new Card(5, "Gold", 1, 1, 0, 0, "Gives 1 Gold"));
        cardList.Add(new Card(6, "White", 2, 0, 1, 0, "Gives 1 Mana"));
        cardList.Add(new Card(7, "Gold", 2, 1, 0, 0, "Gives 1 Gold"));


        cardList.Add(new Card(8, "White 1", 1, 0, 1, 0, "Gives 1 Mana"));
        cardList.Add(new Card(9, "Black 1", 1, 0, 0, 1, "Draw Card"));
        cardList.Add(new Card(10, "Gold 1", 1, 1, 0, 0, "Gives 1 Gold"));
        cardList.Add(new Card(11, "White 2", 2, 0, 1, 0, "Gives 1 Mana"));
        cardList.Add(new Card(12, "Black 2", 2, 0, 0, 1, "Draw Card"));
        cardList.Add(new Card(13, "Gold 2", 2, 1, 0, 0, "Gives 1 Gold"));
    }
}

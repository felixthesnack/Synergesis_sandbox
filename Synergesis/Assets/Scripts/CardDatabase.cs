using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();

    void Awake()
    {
        cardList.Add(new Card(0, "White", 0, 0, 1, 0, "Gives 1 Mana"));
        cardList.Add(new Card(1, "Black", 0, 0, 0, 1, "Draw Card"));
        cardList.Add(new Card(2, "Gold", 0, 1, 0, 0, "Gives 1 Gold"));
        cardList.Add(new Card(3, "White 1", 1, 0, 1, 0, "Gives 1 Mana"));
        cardList.Add(new Card(4, "Black 1", 1, 0, 0, 1, "Draw Card"));
        cardList.Add(new Card(5, "Gold 1", 1, 1, 0, 0, "Gives 1 Gold"));
        cardList.Add(new Card(6, "White 2", 2, 0, 1, 0, "Gives 1 Mana"));
        cardList.Add(new Card(7, "Black 2", 2, 0, 0, 1, "Draw Card"));
        cardList.Add(new Card(8, "Gold 2", 2, 1, 0, 0, "Gives 1 Gold"));
    }
}

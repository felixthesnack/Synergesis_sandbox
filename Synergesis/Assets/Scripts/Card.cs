using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card
{

    public int id;
    public string cardName;
    public int priority;
    public int gold;
    public int mana; 
    public int draws;
    public string description;

    public Card()
    {

    }

    public Card(int Id, string CardName, int Priority, int Gold, int Mana, int Draws, string Description)
    {
        id = Id;
        cardName = CardName;
        priority = Priority;
        gold = Gold;
        mana = Mana;
        draws = Draws;
        description = Description;
    }

}

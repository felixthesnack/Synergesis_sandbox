using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card
{

    public int id;
    public string color;
    public string cardName;
    public int priority;
    public int draws;
    public int mana; 
    public int gold;
    public string description;
    public bool skill = false;
    public int counter;
    public int manaCost;

    public Card()
    {

    }

    public Card(int Id, string Color, string CardName, int Priority, int Draws, int Mana, int Gold, string Description, bool Skill, int Counter, int ManaCost)
    {
        id = Id;
        color = Color;
        cardName = CardName;
        priority = Priority;
        draws = Draws;
        mana = Mana;
        gold = Gold;
        description = Description;
        skill = Skill;
        counter = Counter;
        manaCost = ManaCost;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Card cardDisplay;

    public int id;
    public string cardName;
    public int priority;
    public int gold;
    public int mana;
    public int draws;
    public string description;

    public TMP_Text nameText;
    public TMP_Text priorityText;
    public TMP_Text goldText;
    public TMP_Text manaText;
    public TMP_Text drawsText;
    public TMP_Text descriptionText;
    public Image border;

    //void Start()
    //{
    //    //card.Add(PlayerDeck.staticDeck[0]);
    //    //Debug.Log(PlayerDeck.staticDeck[0].cardName);
    //    //nameText.text = card[0].cardName;
    //    //priorityText.text = card[0].priority.ToString();
    //    //goldText.text = card[0].gold.ToString();
    //    //manaText.text = card[0].mana.ToString();
    //    //drawsText.text = card[0].draws.ToString();
    //    //descriptionText.text = card[0].description.ToString();

    //    //color = nameText.ToString();
    //    //print("card name = " + color);
    //    //Image image = GetComponentInChildren(typeof(Image)) as Image;

    //    //switch (color)
    //    //{
    //    //    case "Black":
    //    //        image.color = new UnityEngine.Color(0f, 0f, 0f);
    //    //        break;
    //    //    case "White":
    //    //        image.color = new UnityEngine.Color(255f, 255f, 255f);
    //    //        break;
    //    //}
    //}

    public void LoadCard(Card cardLoaded)
    {
        cardDisplay = cardLoaded;

        cardName = cardDisplay.cardName;
        priority = cardDisplay.priority;
        gold = cardDisplay.gold;
        mana = cardDisplay.mana;
        draws = cardDisplay.draws;
        description = cardDisplay.description;

        nameText.text = cardName;
        priorityText.text = priority.ToString();
        goldText.text = gold.ToString();
        manaText.text = mana.ToString();
        drawsText.text = draws.ToString();
        descriptionText.text = description;


        switch (cardName)
        {
            case "Black":
                border.color = new UnityEngine.Color(0f, 0f, 0f);
                break;
            case "White":
                border.color = new UnityEngine.Color(255f, 255f, 255f);
                break;
        }

    }


}

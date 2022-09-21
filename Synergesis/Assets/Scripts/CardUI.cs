using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public List<Card> card = new List<Card>();

    public string color; 
    public TMP_Text nameText;
    public TMP_Text priorityText;
    public TMP_Text goldText;
    public TMP_Text manaText;
    public TMP_Text drawsText;
    public TMP_Text descriptionText;
    


    void Start()
    {
        card[0] = PlayerDeck.staticDeck[0];
        nameText.text = card[0].cardName;
        priorityText.text = card[0].priority.ToString();
        goldText.text = card[0].gold.ToString();
        manaText.text = card[0].mana.ToString();
        drawsText.text = card[0].draws.ToString();
        descriptionText.text = card[0].description.ToString();

        color = nameText.ToString();
        print("card name = " + color);
        Image image = GetComponentInChildren(typeof(Image)) as Image;

        switch (color)
        {
            case "Black":
                image.color = new UnityEngine.Color(0f, 0f, 0f);
                break;
            case "White":
                image.color = new UnityEngine.Color(255f, 255f, 255f);
                break;
        }
    }

    //public void LoadCard(Card cardLoaded)
    //{
    //    card[0] = cardLoaded;
    //    nameText.text = card[0].cardName;
    //    priorityText.text = card[0].priority.ToString();
    //    goldText.text = card[0].gold.ToString();
    //    manaText.text = card[0].mana.ToString();
    //    drawsText.text = card[0].draws.ToString();
    //    descriptionText.text = card[0].description.ToString();

    //    color = nameText.ToString();
    //    print("card name = " + color);
    //    Image image = GetComponentInChildren(typeof(Image)) as Image;

    //    switch (color)
    //    {
    //        case "Black":
    //            image.color = new UnityEngine.Color(0f, 0f, 0f);
    //            break;
    //        case "White":
    //            image.color = new UnityEngine.Color(255f, 255f, 255f);
    //            break;
    //    }

    //}


}

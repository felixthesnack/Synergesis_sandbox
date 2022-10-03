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
    public string color;
    public string cardName;
    public int priority;
    public int draws;
    public int mana;
    public int gold;
    public string description;
    public bool skill;

    public TMP_Text nameText;
    public TMP_Text priorityText;
    public TMP_Text goldText;
    public TMP_Text manaText;
    public TMP_Text drawsText;
    public TMP_Text descriptionText;
    
    public GameObject border;

    public Image background;
    public Image priorityBackground;
    public Image priorityBorder;


    public void LoadCard(Card cardLoaded)
    {
        cardDisplay = cardLoaded;

        color = cardDisplay.color;
        cardName = cardDisplay.cardName;
        priority = cardDisplay.priority;
        draws = cardDisplay.draws;
        mana = cardDisplay.mana;
        gold = cardDisplay.gold;
        description = cardDisplay.description;

        nameText.text = cardName;
        priorityText.text = priority.ToString();
        goldText.text = gold.ToString();
        manaText.text = mana.ToString();
        drawsText.text = draws.ToString();
        descriptionText.text = description;


        switch (color)
        {
            case "Black":
                background.color = new UnityEngine.Color(0f, 0f, 0f);
                priorityBackground.color = new UnityEngine.Color(0f, 0f, 0f);
                priorityText.color = new UnityEngine.Color(255f, 255f, 255f);
                nameText.color = new UnityEngine.Color(255f, 255f, 255f);
                descriptionText.color = new UnityEngine.Color(255f, 255f, 255f);
                border.GetComponent<Image>().color = new UnityEngine.Color(255f, 255f, 255f);

                Image[] images = border.GetComponentsInChildren<Image>();

                for (int i = 0; i < images.Length; i++)
                {
                    images[i].color = new UnityEngine.Color(255f, 255f, 255f);
                }
                break;
            case "White":
                background.color = new UnityEngine.Color(255f, 255f, 255f);
                priorityBackground.color = new UnityEngine.Color(255f, 255f, 255f);
                break;
        }

        if(skill == true)

        {
            Image[] images = border.GetComponentsInChildren<Image>();

            for (int i = 0; i < images.Length; i++)
            {
                images[i].material.color = new UnityEngine.Color(255f, 0f, 0f);
            }
        }

    }


}

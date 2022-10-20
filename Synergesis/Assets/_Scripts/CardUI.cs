using UnityEngine;
using TMPro;
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
    public TMP_Text cardTypeText;

    public GameObject Border;

    public GameObject GoldObject;
    public GameObject ManaObject;
    public GameObject DrawsObject;


    public Image background;
    public Image priorityBackground;
    public Image priorityBorder;
    Image[] images;

    Color blackColor = new Color32(0, 0, 0, 255);
    Color whiteColor = new Color32(255, 255, 255, 255);
    Color goldColor = new Color32(255, 206, 114, 255);
    Color redColor = new Color32(255, 0, 0, 255);

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

        if(gold == 0)
        {
            GoldObject.SetActive(false);
        }
        else
        {
            GoldObject.SetActive(true);
        }

        if (mana == 0)
        {
            ManaObject.SetActive(false);
        }
        else
        {
            ManaObject.SetActive(true);
        }

        if (draws == 0)
        {
            DrawsObject.SetActive(false);
        }
        else
        {
            DrawsObject.SetActive(true);
        }

        switch (color)
        {
            case "Black":
                background.color = blackColor;
                priorityBorder.color = whiteColor;
                priorityBackground.color = blackColor;
                priorityText.color = whiteColor;
                nameText.color = whiteColor;
                cardTypeText.color = whiteColor;
                descriptionText.color = whiteColor;
                Border.GetComponent<Image>().color = whiteColor;

                images = Border.GetComponentsInChildren<Image>();

                for (int i = 0; i < images.Length; i++)
                {
                    images[i].color = whiteColor;
                }
                break;
            case "White":
                background.color = whiteColor;
                priorityBackground.color = whiteColor;
                priorityBorder.color = blackColor;
                priorityText.color = blackColor;
                nameText.color = blackColor;
                cardTypeText.color = blackColor;
                descriptionText.color = blackColor;
                Border.GetComponent<Image>().color = blackColor;

                images = Border.GetComponentsInChildren<Image>();

                for (int i = 0; i < images.Length; i++)
                {
                    images[i].color = blackColor;
                }
                break;
            case "Gold":
                background.color = goldColor;
                priorityBackground.color = goldColor;
                priorityBorder.color = blackColor;
                priorityText.color = blackColor;
                nameText.color = blackColor;
                cardTypeText.color = blackColor;
                descriptionText.color = blackColor;
                Border.GetComponent<Image>().color = blackColor;

                images = Border.GetComponentsInChildren<Image>();

                for (int i = 0; i < images.Length; i++)
                {
                    images[i].color = blackColor;
                }
                break;
        }

        if(skill == true)

        {
            Border.GetComponent<Image>().color = redColor;

            images = Border.GetComponentsInChildren<Image>();

            for (int i = 0; i < images.Length; i++)
            {
                images[i].material.color = redColor;
            }

            priorityBorder.color = redColor;
        }

    }


}

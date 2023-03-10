using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckCardUI : CardUI
{
    public GameObject CountText;
    public TMP_Text CountNumber;
    public GameObject MaxCount;

    public TMP_Text LowerPriority;
    public TMP_Text HigherPriority;


    public override void LoadCard(Card cardLoaded)
    {
        base.LoadCard(cardLoaded);

        LowerPriority.text = (cardLoaded.priority - 1).ToString();
        HigherPriority.text = (cardLoaded.priority + 1).ToString();

        if(cardLoaded.color == "Black")
        {
            LowerPriority.color = Color.white;
            HigherPriority.color = Color.white;
        }
        else
        {
            LowerPriority.color = Color.black;
            HigherPriority.color = Color.black;
        }

        if (counter > 1)
        {
            CountText.SetActive(true);
            CountNumber.text = counter.ToString();
            if (counter == 5)
            {
                MaxCount.SetActive(true);
            }
            else
            {
                MaxCount.SetActive(false);
            }
        }
        else
        {
            CountText.SetActive(false);
        }
    }
}

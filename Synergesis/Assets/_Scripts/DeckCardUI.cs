using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckCardUI : CardUI
{
    public GameObject CountText;
    public TMP_Text CountNumber;
    public GameObject MaxCount;
    public GameObject TrashText;
    public TMP_Text TrashCostText;
    public int trashCost = 10;


    public override void LoadCard(Card cardLoaded)
    {
        base.LoadCard(cardLoaded);

        TrashCostText.text = trashCost.ToString();

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

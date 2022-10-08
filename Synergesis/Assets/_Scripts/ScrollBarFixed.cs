using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarFixed : MonoBehaviour

{
    [SerializeField] GameObject ScrollBarUI;
    [SerializeField] PlayerDeck playerDeck;

    int deckCount;

    private void OnEnable()
    {
        deckCount = playerDeck.deck.Count;

        if (deckCount > 12)
        {
            ScrollBarUI.SetActive(true);
            ScrollBarUI.transform.GetComponent<Scrollbar>().size = 0f;
        }
        else
        {
            ScrollBarUI.SetActive(false);
        }
        

    }

}

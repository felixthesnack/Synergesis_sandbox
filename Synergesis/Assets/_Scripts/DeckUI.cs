using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using TMPro;
using System.Reflection;
using UnityEngine.UIElements;

public class DeckUI : MonoBehaviour
{
    public static DeckUI Instance;

    public PlayerDeck playerDeck;
    public CardUI cardUI;
    public List<Card> deckToSort;
    public List<Card> deckToCount;

    private int cardCount = 0;

    public GameObject DeckUIContainer;

    public int blackCount = 0;
    public int whiteCount = 0;
    public int goldCount = 0;
    public int colorlessCount = 0;

    public int priorityEditCost = 25;
    public int trashEditCost = 10;
    public TMP_Text priorityEditCostText;
    public TMP_Text trashEditCostText;

    public TMP_Text blackCountText;
    public TMP_Text whiteCountText;
    public TMP_Text goldCountText;
    public TMP_Text colorlessCountText;

    public GameObject CardSlotContainer;
    public GameObject CardSlot;

    public GameObject ScrollBarUI;

    public GameObject ZoomPanel;


    public static Action SortDeck;


    private void Awake()
    {
        Instance = this;
        priorityEditCostText.text = priorityEditCost.ToString();
        trashEditCostText.text = trashEditCost.ToString();
    }

    //private void OnEnable()
    //{
    //    //deckToSort = playerDeck.deck.Distinct().ToList();
    //    LoadStaticDeck();
    //    SortDeck?.Invoke();
    //    LoadSortedDeckUI();

    //}



    public void LoadSortedDeckUI()
    {        

        int sortedDeckCount = deckToSort.Count;
        for (int i = 0; i < sortedDeckCount; i++)
        {
            GameObject index = DeckUIContainer.transform.GetChild(i).transform.GetChild(0).gameObject;
            DeckCardUI cardSlotUI = index.GetComponent<DeckCardUI>();
            ZoomCard zoomCard = index.GetComponent<ZoomCard>();

            cardSlotUI.LoadCard(deckToSort[i]);
            zoomCard.parentIndex = i;
        }
    }

    public void LoadStaticDeck()
    {
        blackCount = 0;
        whiteCount = 0;
        goldCount = 0;
        colorlessCount = 0;
        
        deckToCount = new List<Card>(playerDeck.deck);
        
        deckToSort.Clear();
        //deckToSort.Add(deckToCount[0]);

        //for (int i = 1; i < deckToCount.Count - 1; i++)
        //{
        //    if (deckToSort[0].id != deckToCount[i].id)
        //    {
                
        //    }
        //}

        deckToSort = new List<Card>(playerDeck.deck.GroupBy(x =>x.id).Select(y => y.First()).ToList());

        //deckToSort.AddRange(playerDeck.deck.Distinct().ToList());

        int cardSlotCount = DeckUIContainer.transform.childCount;
        int deckToSortCount = deckToSort.Count;
        

        if (cardSlotCount < deckToSortCount)
        {
            for (int i = 0; i < (deckToSortCount - cardSlotCount); i++)
            {
                GameObject cardSlotContainer = Instantiate(CardSlotContainer, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation, DeckUIContainer.transform) as GameObject;
                GameObject cardSlot = Instantiate(CardSlot, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation, cardSlotContainer.transform) as GameObject;
                cardSlot.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
                //cardSlot.transform.SetParent(cardSlotContainer.transform);
                //cardSlot.transform.position = new Vector3(0f, 0f, 0f);

                //ZoomCard zoomCard = cardSlot.GetComponent<ZoomCard>();
                //zoomCard.parentIndex = i;
            }
        }
        else if (cardSlotCount > deckToSortCount)
        {
            for(int j = cardSlotCount; j > deckToSortCount; j--)
            {
                Destroy(DeckUIContainer.transform.GetChild(j - 1).gameObject);
                cardSlotCount--;
            }
        }

        for (int i = 0; i < deckToSortCount; i++)
        {
            while (deckToCount.Contains(deckToSort[i]))
            {
                string color = deckToSort[i].color;
                switch (color)
                {
                    case "Black":
                        blackCount++;
                        break;
                    case "White":
                        whiteCount++;
                        break;
                    case "Gold":
                        goldCount++;
                        break;
                    case "Colorless":
                        colorlessCount++;
                        break;
                }

                int id = deckToSort[i].id;
                int xIndex = deckToCount.FindIndex(x => x.id == id);
                deckToCount.Remove(deckToCount[xIndex]);

                cardCount++;
            }

            Card tempCard = deckToSort[i];
            tempCard.counter = cardCount;
            deckToSort[i] = tempCard;

            cardCount = 0;
        }

        blackCountText.text = blackCount.ToString();
        whiteCountText.text = whiteCount.ToString();
        goldCountText.text = goldCount.ToString();
        colorlessCountText.text = colorlessCount.ToString();
    }

    public void SortByPriorityDescending()
    {
        List<Card> sortedDeck = deckToSort.OrderByDescending(card => card.priority).ToList();
        deckToSort = sortedDeck;
    }

    public void SortByPriorityAcending()
    {
        List<Card> sortedDeck = deckToSort.OrderBy(card => card.priority).ToList();
        deckToSort = sortedDeck;
    }

    public void SortByColorDescending()
    {
        List<Card> sortedDeck = deckToSort.OrderByDescending(card => card.color).ToList();
        deckToSort = sortedDeck;
    }

    public void SortByColorAcending()
    {
        List<Card> sortedDeck = deckToSort.OrderBy(card => card.color).ToList();
        deckToSort = sortedDeck;
    }
    public void SortByDrawsDescending()
    {
        List<Card> sortedDeck = deckToSort.OrderByDescending(card => card.draws).ToList();
        deckToSort = sortedDeck;
    }

    public void SortByDrawsAcending()
    {
        List<Card> sortedDeck = deckToSort.OrderBy(card => card.draws).ToList();
        deckToSort = sortedDeck;
    }

    public void SortByGoldDescending()
    {
        List<Card> sortedDeck = deckToSort.OrderByDescending(card => card.gold).ToList();
        deckToSort = sortedDeck;
    }

    public void SortByGoldAcending()
    {
        List<Card> sortedDeck = deckToSort.OrderBy(card => card.gold).ToList();
        deckToSort = sortedDeck;
    }

    public void SortByManaDescending()
    {
        List<Card> sortedDeck = deckToSort.OrderByDescending(card => card.mana).ToList();
        deckToSort = sortedDeck;
    }

    public void SortByManaAcending()
    {
        List<Card> sortedDeck = deckToSort.OrderBy(card => card.mana).ToList();
        deckToSort = sortedDeck;
    }
}
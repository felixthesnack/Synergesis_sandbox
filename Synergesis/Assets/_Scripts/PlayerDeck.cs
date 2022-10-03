using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerDeck : MonoBehaviour
{
    public List<Card> container = new List<Card>();

    public int[] starterCards = { 0, 0, 1, 1, 3, 3, 4, 4, 2, 5 };
    public List<Card> starterDeck = new List<Card>();

    public List<Card> deck = new List<Card>();
    public List<Card> staticDeck = new List<Card>();

    public int cardsInHandCount = 0;
    public List<GameObject> cardsInHand = new List<GameObject>();

    public int deckSize;
    public static int priority;
    public int priority1count = 0;
    public int priority2count = 0;
    public int priority3count = 0;
    public int containerIndex = 0;

    public GameObject DrawDeck;

    public GameObject DeckUIContainer;

    public GameObject Hand;
    public GameObject CardToHandContainer;
    public GameObject CardPlayed;

    public CardUI cardUI;
    public DeckUI deckUI;

    public Button drawButton;

    //public float drawInterval = 1f;
    public float cardAnim = 0.25f;
    public float handAnim = 0.75f;

    public Transform containerPosition;

    private string iTest = "InvokeTest";

    // Start is called before the first frame update

    void Start()
    {
        drawButton.onClick.AddListener(() => DrawCards(1));

        foreach(var x in starterCards)
        {
        starterDeck.Add(CardDatabase.cardList[x]);
        }

        deck = starterDeck;
        staticDeck = starterDeck;
        deckSize = deck.Count;

        deckUI.LoadStaticDeckUI();

        Invoke(iTest, 0);

        //Shuffle();
        //DrawCards(5, 1f);
        //ReadyCard();

        StartCoroutine(StartTurn());
    }


    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator StartTurn()
    {
        yield return new WaitForSeconds(2f);
        Shuffle();
        yield return StartCoroutine(DrawCard(5));
        ReadyCard();
    }

    public void DrawCards(int cards)
    {
            StartCoroutine(DrawCard(cards));
    }

    IEnumerator DrawCard(int cards)
    {
        if (deckSize > 0)
        {
            for (int i = 0; i < cards; i++)

            {

                GameObject cardContainer = Instantiate(CardToHandContainer) as GameObject;
                GameObject card = Instantiate(CardPlayed, DrawDeck.transform.position, transform.rotation) as GameObject;

                cardUI = card.GetComponent<CardUI>();
                //card.transform.localScale = Vector3.one;

                cardUI.LoadCard(deck[deckSize - 1]);

                cardsInHand.Add(card);

                card.name = "Card";

                cardContainer.name = "CardContainer";
                cardContainer.transform.SetParent(Hand.transform);
                cardContainer.transform.localScale = Vector3.one;
                cardContainer.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                cardContainer.transform.eulerAngles = new Vector3(0, 0, 0);

                yield return new WaitForFixedUpdate();

                priority = deck[deckSize - 1].priority;

                cardsInHandCount = cardsInHand.Count - 1;

                switch (priority)
                {
                    case 1:
                        containerIndex = priority2count + priority3count;
                        cardContainer.transform.SetSiblingIndex(containerIndex);
                        priority1count++;
                        print("priority1count = " + priority1count + ", priority2count = " + priority2count + ", priority3count = " + priority3count);
                        break;
                    case 2:
                        containerIndex = cardsInHandCount - priority1count - priority2count;
                        cardContainer.transform.SetSiblingIndex(containerIndex);
                        priority2count++;
                        print("priority1count = " + priority1count + ", priority2count = " + priority2count + ", priority3count = " + priority3count);
                        break;
                    case 3:
                        containerIndex = priority3count;
                        cardContainer.transform.SetSiblingIndex(containerIndex);
                        priority3count++;
                        print("priority1count = " + priority1count + ", priority2count = " + priority2count + ", priority3count = " + priority3count);
                        break;


                        // reverse sorting
                        //case 0:
                        //    card.transform.SetSiblingIndex(priority1count);
                        //    priority1count++;
                        //    //print ("priority1count = " + priority1count + ", priority2count = " + priority2count + ", priority3count = " + priority3count);
                        //    break;
                        //case 1:
                        //    card.transform.SetSiblingIndex(priority1count + priority2count);
                        //    priority2count++;
                        //    //print("priority1count = " + priority1count + ", priority2count = " + priority2count + ", priority3count = " + priority3count);
                        //    break;
                        //case 2:
                        //    card.transform.SetSiblingIndex(priority1count + priority2count + priority3count);
                        //    priority3count++;
                        //    //print("priority1count = " + priority1count + ", priority2count = " + priority2count + ", priority3count = " + priority3count);
                        //    break;
                }

                containerPosition = cardContainer.transform;
                LayoutElement cardLayout = cardContainer.GetComponent<LayoutElement>();
                Vector2 preferredSize = new Vector2(200, 300);

                yield return new WaitForFixedUpdate();

                card.transform.SetParent(containerPosition);

                //card.transform.parent = cardContainer.transform;
                card.transform.localScale = new Vector3(0.4f, 0.4f);

                cardLayout.DOPreferredSize(preferredSize, handAnim).SetEase(Ease.InOutSine);

                Sequence putCardInHand = DOTween.Sequence();
                putCardInHand.Join(card.transform.DOLocalMove(new Vector3(0, 0, 0), cardAnim).SetEase(Ease.OutBack));
                putCardInHand.Join(card.transform.DOScale(Vector3.one, cardAnim).SetEase(Ease.OutBack));

                yield return putCardInHand.WaitForCompletion();

                //card.transform.DOLocalMove(new Vector3(0, 0, 0), cardAnim).SetEase(Ease.OutBack);
                //card.transform.DOScale(Vector3.one, cardAnim).SetEase(Ease.OutBack);

                deckSize--;

                //if(cardUI.draws != 0)
                //{
                //    cards += cardUI.draws;
                //}


                print("deck size = " + deckSize);

            }
        }
    }

    public void ReadyCard()
    {
        var lastChild = Hand.transform.GetChild(Hand.transform.childCount -1);
        var firstCard = lastChild.transform.GetChild(0);
        //RectTransform playCard = firstCard.transform.GetComponent<RectTransform>();
        //playCard.anchorMin = new Vector2(0, 0);
        //playCard.anchorMax = new Vector2(0, 0);
        //playCard.pivot = new Vector2(0, 0);
        firstCard.transform.DOScale(1.2f, 0.5f);
    }
    //private void LoadDeckUI()
    //{
    //    for (int i = 0; i < deckSize; i++)
    //    {
    //        GameObject cardSlot = Instantiate(CardPlayed, transform.position, transform.rotation, DrawDeckUI.transform) as GameObject;
    //        cardUI = cardSlot.GetComponent<CardUI>();
    //        //card.transform.localScale = Vector3.one;

    //        cardUI.LoadCard(staticDeck[i]);
    //    }
    //}

    public void InvokeTest()
    {
        Debug.Log("working");
    }

    public void Shuffle()
    {
        for(int i = 0; i < deckSize; i++)
        {
            container[0] = deck[i];
            int randomIndex = Random.Range(i, deckSize);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = container[0];
        }
    }
}

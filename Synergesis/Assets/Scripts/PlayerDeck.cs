using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerDeck : MonoBehaviour
{
    public List<Card> container = new List<Card>();
    public List<Card> deck = new List<Card>();
    public List<Card> starterDeck = new List<Card>();

    public int cardsInHandCount = 0;
    public List<GameObject> cardsInHand = new List<GameObject>();

    public int deckSize;
    public static int priority;
    public int priority0count = 0;
    public int priority1count = 0;
    public int priority2count = 0;
    public int containerIndex = 0;


    public GameObject Hand;
    public GameObject CardToHandContainer;
    public GameObject DrawDeck;

    public GameObject TestCard;
    public GameObject TestCardContainer;
    public CardUI cardUI;

    public Button drawButton;

    //public float drawInterval = 1f;
    public float cardAnim = 1f;
    public float handAnim = 0.75f;

    public Transform containerPosition;

    private string iTest = "InvokeTest";

    // Start is called before the first frame update

    void Start()
    {
        drawButton.onClick.AddListener(() => DrawCards(1, 0f));

        //StartCoroutine(StarterDeck());

        starterDeck.Add(CardDatabase.cardList[2]);
        starterDeck.Add(CardDatabase.cardList[5]);
        starterDeck.Add(CardDatabase.cardList[0]);
        starterDeck.Add(CardDatabase.cardList[0]);
        starterDeck.Add(CardDatabase.cardList[1]);
        starterDeck.Add(CardDatabase.cardList[1]);
        starterDeck.Add(CardDatabase.cardList[3]);
        starterDeck.Add(CardDatabase.cardList[3]);
        starterDeck.Add(CardDatabase.cardList[4]);
        starterDeck.Add(CardDatabase.cardList[4]);

        deck = starterDeck;
        deckSize = deck.Count;

        Invoke(iTest, 0);
        Shuffle();
        DrawCards(5, 1f);

    }

    // Update is called once per frame
    void Update()
    {

    }

    //public IEnumerator StartGame()
    //{
    //    //drawDeck = DrawDeck.transform.position;

    //    for (int i = 0; i < 5; i++)
    //    {
    //        yield return new WaitForSeconds(drawInterval);
    //        StartCoroutine(DrawCard());
    //    yield return null;
    //}

    //IEnumerator StarterDeck()
    //{
    //    starterDeck.AddRange(new List<Card>
    //        { 
    //        CardDatabase.cardList[2],
    //        CardDatabase.cardList[5],
    //        CardDatabase.cardList[0],
    //        CardDatabase.cardList[0],
    //        CardDatabase.cardList[1],
    //        CardDatabase.cardList[1],
    //        CardDatabase.cardList[3],
    //        CardDatabase.cardList[3],
    //        CardDatabase.cardList[4],
    //        CardDatabase.cardList[4]
    //    });

    //    yield return new WaitForFixedUpdate();
    //}

    public void DrawCards(int cards, float drawInterval)
    {
            StartCoroutine(DrawCard(cards, drawInterval));
    }
    IEnumerator DrawCard(int cards, float drawInt)
    {
        if (deckSize > 0)
        {
            for (int i = 0; i < cards; i++)

            {

                //GameObject testCard = Instantiate(TestCard, TestCardContainer.transform.position, Quaternion.identity) as GameObject;
                //testCard.transform.parent = TestCardContainer.transform;

                //testCard.transform.localScale = Vector3.one;

                //cardUI = testCard.GetComponent<CardUI>();

                //cardUI.LoadCard(deck[0]);

                yield return Utilities.GetWait(drawInt);

                GameObject cardContainer = Instantiate(CardToHandContainer) as GameObject;
                GameObject card = Instantiate(TestCard, DrawDeck.transform.position, transform.rotation) as GameObject;

                cardUI = card.GetComponent<CardUI>();
                card.transform.localScale = Vector3.one;

                cardUI.LoadCard(deck[deckSize - 1]);

                cardsInHand.Add(card);

                card.name = "Card";

                cardContainer.name = "CardContainer";
                cardContainer.transform.SetParent(Hand.transform);
                cardContainer.transform.localScale = Vector3.one;
                cardContainer.transform.position = new Vector3(transform.position.x, transform.position.y, -48);
                cardContainer.transform.eulerAngles = new Vector3(25, 0, 0);

                yield return new WaitForFixedUpdate();

                priority = deck[deckSize - 1].priority;

                cardsInHandCount = cardsInHand.Count - 1;

                switch (priority)
                {
                    case 0:
                        containerIndex = priority1count + priority2count;
                        cardContainer.transform.SetSiblingIndex(containerIndex);
                        priority0count++;
                        print("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count);
                        break;
                    case 1:
                        containerIndex = cardsInHandCount - priority0count - priority1count;
                        cardContainer.transform.SetSiblingIndex(containerIndex);
                        priority1count++;
                        print("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count);
                        break;
                    case 2:
                        containerIndex = priority2count;
                        cardContainer.transform.SetSiblingIndex(containerIndex);
                        priority2count++;
                        print("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count);
                        break;

                        //card sorting
                        //case 0:
                        //    card.transform.SetSiblingIndex(priority1count + priority2count);
                        //    priority0count++;
                        //    //print ("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count);
                        //    break;
                        //case 1:
                        //    card.transform.SetSiblingIndex(cardsInHandCount - priority0count - priority1count);
                        //    priority1count++;
                        //    //print("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count);
                        //    break;
                        //case 2:
                        //    card.transform.SetSiblingIndex(priority2count);
                        //    priority2count++;
                        //    //print("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count);
                        //    break;

                        // reverse sorting
                        //case 0:
                        //    card.transform.SetSiblingIndex(priority0count);
                        //    priority0count++;
                        //    //print ("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count);
                        //    break;
                        //case 1:
                        //    card.transform.SetSiblingIndex(priority0count + priority1count);
                        //    priority1count++;
                        //    //print("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count);
                        //    break;
                        //case 2:
                        //    card.transform.SetSiblingIndex(priority0count + priority1count + priority2count);
                        //    priority2count++;
                        //    //print("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count);
                        //    break;
                }

                containerPosition = cardContainer.transform;
                LayoutElement cardLayout = cardContainer.GetComponent<LayoutElement>();
                Vector2 preferredSize = new Vector2(200, 300);

                yield return new WaitForFixedUpdate();

                card.transform.SetParent(containerPosition);

                //card.transform.parent = cardContainer.transform;
                card.transform.localScale = new Vector3(0.3f, 0.3f);

                cardLayout.DOPreferredSize(preferredSize, handAnim).SetEase(Ease.InOutSine);

                card.transform.DOLocalMove(new Vector3(0, 0, 0), cardAnim).SetEase(Ease.OutBack);
                card.transform.DOScale(Vector3.one, cardAnim).SetEase(Ease.OutBack);


                deckSize--;

                if(cardUI.draws != 0)
                {
                    cards += cardUI.draws;
                }


                print("deck size = " + deckSize);

            }
        }
    }
    //IEnumerator AnimateContainer(Vector2 endValue, LayoutElement container, float duration)
    //{
    //    container.DOPreferredSize(endValue, duration).SetEase(Ease.InOutSine);
    //    yield return null;

    //}

    //IEnumerator AnimateCard(Transform start, Vector3 end, float t)
    //{
    //    start.DOLocalMove(end, t).SetEase(Ease.InOutBack);
    //    yield return null;
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

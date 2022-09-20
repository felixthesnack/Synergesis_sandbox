using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerDeck : MonoBehaviour
{
    public List<Card> container = new List<Card>();
    private int x;
    public static int deckSize;
    public List<Card> deck = new List<Card>();
    public List<Card> starterDeck = new List<Card>();

    public List<GameObject> cardsInHand = new List<GameObject>();
    public static List<Card> staticDeck = new List<Card>();
    public int cardCount = 0;

    public static int priority;
    public string color;
    public int priority0count = 0;
    public int priority1count = 0;
    public int priority2count = 0;
    public int containerIndex = 0;

    public GameObject CardToHandContainer; 
    public GameObject CardToHand;
    public GameObject Hand;
    public GameObject DrawDeck;

    public float drawInterval = 1f;
    public float cardAnim = 1f;
    public float handAnim = 0.75f;

    public Transform containerPosition;

    private string iTest = "InvokeTest";

    // Start is called before the first frame update
    void Start()
    {
        //x = 0;
        //deckSize = 40;

        //for (int i = 0; i < deckSize; i++)
        //{
        //    x = Random.Range(1, 8);
        //    deck[i] = CardDatabase.cardList[x];

        //}
        //for (int i = 0; i < 4; i++)
        //{
        //    starterDeck.Add(CardDatabase.cardList[0]);
        //}
        //for (int i = 0; i < 4; i++)
        //{
        //    starterDeck.Add(CardDatabase.cardList[1]);
        //}
        //for (int i = 0; i < 2; i++)
        //{
        //    starterDeck.Add(CardDatabase.cardList[2]);
        //}
        starterDeck.AddRange(new List<Card>
        {
            CardDatabase.cardList[2],
            CardDatabase.cardList[5],
            CardDatabase.cardList[0],
            CardDatabase.cardList[0],
            CardDatabase.cardList[1],
            CardDatabase.cardList[1],
            CardDatabase.cardList[3],
            CardDatabase.cardList[3],
            CardDatabase.cardList[4],
            CardDatabase.cardList[4]
        });

        deck = starterDeck;
        deckSize = deck.Count;

        Invoke(iTest, 0);
        Shuffle();
        DrawCards(5);
    }

    // Update is called once per frame
    void Update()
    {
        staticDeck = deck;
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

    public void DrawCards(int cards)
    {
            StartCoroutine(DrawCard(cards));
    }
    IEnumerator DrawCard(int cards)
    {
        for (int i = 0; i < cards; i++)
        {
            GameObject cardContainer = Instantiate(CardToHandContainer) as GameObject;
            GameObject card = Instantiate(CardToHand, DrawDeck.transform.position, transform.rotation) as GameObject;
            yield return Utilities.GetWait(drawInterval);

            //GameObject card = Instantiate(CardToHand, drawDeck, transform.rotation) as GameObject;
            cardContainer.name = "CardContainer";

            cardsInHand.Add(card);
            cardCount = cardsInHand.Count - 1;
            card.name = "Card";

            cardContainer.transform.SetParent(Hand.transform);
            cardContainer.transform.localScale = Vector3.one;
            cardContainer.transform.position = new Vector3(transform.position.x, transform.position.y, -48);
            cardContainer.transform.eulerAngles = new Vector3(25, 0, 0);

            yield return new WaitForFixedUpdate();

            color = cardsInHand[cardCount].GetComponent<DisplayCard>().cardName;

            switch (color)
            {
                case "Black":
                    cardsInHand[cardCount].transform.Find("Border").GetComponent<Image>().color = new Color(0, 0, 0);
                    break;
                case "White":
                    cardsInHand[cardCount].transform.Find("Border").GetComponent<Image>().color = new Color(255, 255, 255);
                    break;
            }

            priority = cardsInHand[cardCount].GetComponent<DisplayCard>().priority;

            print("card name = " + color + ", priority = " + priority);

            switch (priority)
            {
                case 0:
                    containerIndex = priority1count + priority2count;
                    cardContainer.transform.SetSiblingIndex(containerIndex);
                    priority0count++;
                    print("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count);
                    break;
                case 1:
                    containerIndex = cardCount - priority0count - priority1count;
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
                    //    card.transform.SetSiblingIndex(cardCount - priority0count - priority1count);
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

            //LayoutElement cardWidth = cardContainer.GetComponent<LayoutElement>();
            //cardWidth.preferredWidth =  Mathf.Lerp(0, 200, 1.75f);

            //loop through child layout elements
            //LayoutElement[] myLayoutElements = Hand.GetComponentsInChildren<LayoutElement>();

            ////For each child in the array change its LayoutElement's preferred width size to 250.
            //foreach (LayoutElement element in myLayoutElements)
            //{
            //    element.preferredWidth = Mathf.Lerp(0, 200, 5f);
            //}

            //Vector3 containerPosition = new Vector3(cardContainer.transform.GetChild(containerIndex).position.x, cardContainer.transform.GetChild(containerIndex).position.y);
            containerPosition = cardContainer.transform;
            LayoutElement cardLayout = cardContainer.GetComponent<LayoutElement>();
            Vector2 preferredSize = new Vector2(200, 300);

            //print("container index = " + cardContainer.transform.GetSiblingIndex() +", container position = " + containerPosition);
            yield return new WaitForFixedUpdate();
            //print("container index = " + cardContainer.transform.GetSiblingIndex() + ", container position = " + containerPosition);
            card.transform.SetParent(containerPosition);
            card.transform.localScale = Vector3.one;

            //StartCoroutine(AnimateContainer(preferredSize, cardLayout, handAnim));
            cardLayout.DOPreferredSize(preferredSize, handAnim).SetEase(Ease.InOutSine);

            //StartCoroutine(AnimateCard(card.transform, new Vector3(0, 0, 0), cardAnim));
            card.transform.DOLocalMove(new Vector3(0, 0, 0), cardAnim).SetEase(Ease.OutBack);

            //card.transform.DOMove(containerPosition, 2);
            //yield return new WaitForFixedUpdate();

            print("deckCount = " + deckSize);
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

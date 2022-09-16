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
    public List<GameObject> cardsInHand = new List<GameObject>();
    public static List<Card> staticDeck = new List<Card>();
    public int cardCount = 0;

    public static int priority;
    public int priority0count = 0;
    public int priority1count = 0;
    public int priority2count = 0;
    public int containerIndex = 0;

    public GameObject CardToHandContainer; 
    public GameObject CardToHand;
    public GameObject Hand;

    public Transform containerPosition;

    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        deckSize = 40;

        for (int i = 0; i < deckSize; i++)
        {
            x = Random.Range(1, 8);
            deck[i] = CardDatabase.cardList[x];

        }

        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        staticDeck = deck;
    }

    IEnumerator StartGame()
    {
        //drawDeck = DrawDeck.transform.position;

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(2);
            GameObject card = Instantiate(CardToHand, transform.position, transform.rotation) as GameObject;
            GameObject cardContainer = Instantiate(CardToHandContainer, transform.position, transform.rotation) as GameObject;

            //GameObject card = Instantiate(CardToHand, drawDeck, transform.rotation) as GameObject;
            cardContainer.name = "CardContainer";
            cardsInHand.Add(card);
            cardCount = cardsInHand.Count-1;
            card.name = "Card";
            yield return new WaitForFixedUpdate();
            priority = cardsInHand[cardCount].GetComponent<DisplayCard>().priority;

            print("card count = " + cardCount);
            print("priority = " + priority);

            switch (priority)
            {
                case 0:
                    containerIndex = priority1count + priority2count;
                    cardContainer.transform.SetSiblingIndex(containerIndex);
                    priority0count++;
                    print ("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count);
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
            StartCoroutine(AnimateContainer(preferredSize, cardLayout, 1f));
            StartCoroutine(TweenCard(card.transform, new Vector3(0,0,0), 1.5f));
            //card.transform.DOMove(containerPosition, 2);
            //yield return new WaitForFixedUpdate();


        }
    }

    IEnumerator AnimateContainer(Vector2 endValue, LayoutElement container, float duration)
    {
        container.DOPreferredSize(endValue, duration);
        yield return null;

    }

    IEnumerator TweenCard(Transform start, Vector3 end, float t)
    {
        start.DOLocalMove(end, t).SetEase(Ease.OutBounce);
        yield return null;
    }

    public static float EaseInOut(float initial, float final, float time, float duration)
    {
        float change = final - initial;
        time /= duration / 2;
        if (time < 1f) return change / 2 * time * time + initial;
        time--;
        return -change / 2 * (time * (time - 2) - 1) + initial;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public List<Card> container = new List<Card>();
    public int x;
    public static int deckSize;
    public List<Card> deck = new List<Card>();
    public List<GameObject> cardsInHand = new List<GameObject>();
    public static List<Card> staticDeck = new List<Card>();
    public int cardCount = 0;

    public static int priority;
    public int priority0count = 0;
    public int priority1count = 0;
    public int priority2count = 0;

    public GameObject cardInDeck1;
    public GameObject cardInDeck2;
    public GameObject cardInDeck3;
    public GameObject cardInDeck4;

    public GameObject CardToHand;
    public GameObject[] Clones;
    public GameObject Hand;

    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        deckSize = 40;

        for (int i = 0; i < deckSize; i++)
        {
            x = Random.Range(1, 9);
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
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1);
            GameObject card = Instantiate(CardToHand, transform.position, transform.rotation) as GameObject;
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
                    card.transform.SetSiblingIndex(priority1count + priority2count);
                    priority0count++;
                    //print ("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count);
                    break;
                case 1:
                    card.transform.SetSiblingIndex(cardCount - priority0count - priority1count);
                    priority1count++;
                    //print("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count);
                    break;
                case 2:
                    card.transform.SetSiblingIndex(priority2count); 
                    priority2count++;
                    //print("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count);
                    break;
            }

        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Reflection;

public class PlayerDeck : MonoBehaviour
{
    public static PlayerDeck Instance;

    private List<Card> container = new List<Card>();
    public int[] starterCards = { 0, 0, 1, 1, 3, 3, 4, 4, 2, 5 };
    [SerializeField] List<Card> starterDeck = new List<Card>();

    public List<Card> deck = new List<Card>();
    public List<Card> staticDeck = new List<Card>();

    public int cardsInHandCount = 0;
    //public List<GameObject> cardsInHand = new List<GameObject>();

    public int deckSize;
    public int cardsDrawn;
    public int cardsPlayed = 0;
    public static int priority;
    public int priority0count = 0;
    public int priority1count = 0;
    public int priority2count = 0;
    public int priority3count = 0;
    public int priority4count = 0;
    public int containerIndex;

    public int synergyLevel = 20;
    public int maxHealth;
    public int currentHealth;

    public HealthBar healthBar;
    public CountersUI counters;

    public float cardAnim = 0.25f;
    public float handAnim = 0.75f;

    public float playCardDrop = -2f;
    public float playCardDropSpeed = 0.3f;
    public float playCardContainerSpeed = 0.15f;
    public float cardPunch = 0.1f;
    public bool drawIsRunning = false;

    public GameObject DrawDeck;

    public GameObject Hand;
    public GameObject CardToHandContainer;
    public GameObject CardPlayed;
    public GameObject DeckView;
    public GameObject PlayArea;
    public GameObject DraftCanvas;
    public GameObject DeckButton;
    public GameObject WinScreen; //create + LoseScreen

    public CoroutineQueue queue;
    public bool startIsRunning = false;

    public SlotManager slotManager;
    private CardUI cardUI;

    public Button drawButton;

    private Transform containerPosition;

    private string iTest = "InvokeTest";

    // Start is called before the first frame update

    void Awake()
    {
        Instance = this;
        GameManager.OnGameStateChanged += SetBattleState;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= SetBattleState;
    }

    private void SetBattleState(GameState state)
    {
        if(state == GameState.Battle)
        {
            ResetState();
            StartCoroutine(StartTurn());
        }
    }

    void Start()
    {
        DeckUI.Instance.LoadStaticDeck();
        DeckView.SetActive(false);

        PlayArea.SetActive(false);

        queue = new CoroutineQueue(this);
        queue.StartLoop();

        drawButton.onClick.AddListener(() => StartCoroutine(DrawCards(1)));
        PlayArea.GetComponent<Button>().onClick.AddListener(() => {
            queue.EnqueueAction(PlayCard());
            //Debug.Log("Action Enqueued");
        });

        foreach (var x in starterCards)
        {
            starterDeck.Add(CardDatabase.cardList[x]);
        }


        container.Add(new Card());

        deck.AddRange(starterDeck);
        deckSize = deck.Count;

        Invoke(iTest, 0);

        slotManager.LoadSynergyBar();

        maxHealth = (int)(Mathf.Round(((27 + (synergyLevel - 6) * (synergyLevel + 7) / 2)/25)*25)/2);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth);

    }

    void Update()
    {
        if (!PauseMenu.autoPlay)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                queue.EnqueueAction(PlayCard());
            }
        }
    }

    public IEnumerator StartTurn()
    {
        startIsRunning = true;
        yield return new WaitForSeconds(1.25f);
        Shuffle();
        yield return StartCoroutine(DrawCard(5));
        ReadyCard(Hand.transform.childCount - 1, true);
        yield return new WaitForFixedUpdate();

        if (!PauseMenu.autoPlay) 
        { 
            //queue.StopLoop();
            PlayArea.SetActive(true); 
        }
        else
        {
            CheckQueue();
        }
        startIsRunning = false;
    }

    public IEnumerator DrawCards(int cards)
    {
        drawIsRunning = true;
        yield return StartCoroutine(DrawCard(cards));

       // int cardIndex = containerPosition.GetSiblingIndex();
        //Debug.Log("card index = " + containerIndex);
        if (cardsInHandCount > 1)
        {
            if (containerIndex == cardsInHandCount - 1)
            {
                ReadyCard(containerIndex - 1, false);
            }
            //else { }
        }
        ReadyCard(cardsInHandCount - 1, true);
        drawIsRunning = false;
    }

    IEnumerator DrawCard(int cards)
    {
        if (cardsDrawn < deckSize)
        {
            for (int i = 0; i < cards; i++)

            {
                GameObject cardContainer = Instantiate(CardToHandContainer, transform.position, transform.rotation, Hand.transform);
                cardContainer.name = "CardContainer";
                cardContainer.transform.localScale = new Vector3(1,1,0);

                containerPosition = cardContainer.transform;

                GameObject card = Instantiate(CardPlayed, DrawDeck.transform.position, transform.rotation);
                card.name = "Card";
                card.transform.localScale = new Vector3(0.4f, 0.4f, 1);

                //cardsInHand.Add(card);
                cardsInHandCount++;

                cardUI = card.GetComponent<CardUI>();
                cardUI.LoadCard(deck[cardsDrawn]);

                cardsDrawn++;

                yield return new WaitForFixedUpdate();
                
                //card.transform.SetParent(containerPosition);

                priority = cardUI.priority;

                switch (priority)
                {
                    case 0:
                        containerIndex = priority1count + priority2count + priority3count + priority4count;
                        containerPosition.SetSiblingIndex(containerIndex);
                        priority1count++;
                        //print("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count + ", priority3count = " + priority3count + ", priority4count = " + priority4count);
                        break;
                    case 1:
                        containerIndex = priority2count + priority3count + priority4count;
                        containerPosition.SetSiblingIndex(containerIndex);
                        priority1count++;
                        //print("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count + ", priority3count = " + priority3count + ", priority4count = " + priority4count);
                        break;
                    case 2:
                        containerIndex = priority3count + priority4count;
                        containerPosition.SetSiblingIndex(containerIndex);
                        priority2count++;
                        //print("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count + ", priority3count = " + priority3count + ", priority4count = " + priority4count);
                        break;
                    case 3:
                        containerIndex = priority4count;
                        containerPosition.SetSiblingIndex(containerIndex);
                        priority3count++;
                        //print("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count + ", priority3count = " + priority3count + ", priority4count = " + priority4count);
                        break;
                    case 4:
                        containerIndex = 0;
                        containerPosition.SetSiblingIndex(containerIndex);
                        priority3count++;
                        //print("priority0count = " + priority0count + ", priority1count = " + priority1count + ", priority2count = " + priority2count + ", priority3count = " + priority3count + ", priority4count = " + priority4count);
                        break;
                }
                //Debug.Log("internal card index = " + containerIndex);

                LayoutElement cardLayout = cardContainer.GetComponent<LayoutElement>();
                Vector2 preferredSize = new Vector2(190, 300);

                yield return new WaitForFixedUpdate();

                card.transform.SetParent(containerPosition);

                //card.transform.parent = cardContainer.transform;
                if (!PauseMenu.autoPlay || startIsRunning == true)
                {
                    cardLayout.DOPreferredSize(preferredSize, handAnim).SetEase(Ease.InOutSine);
                }
                else
                {
                    cardLayout.DOPreferredSize(preferredSize, playCardContainerSpeed).SetEase(Ease.InOutSine);
                }

                Sequence putCardInHand = DOTween.Sequence();
                putCardInHand.Join(card.transform.DOLocalMove(new Vector3(200, 0), cardAnim * 0.6f).SetEase(Ease.OutQuad))
                .Join(card.transform.DOScale(Vector3.one, cardAnim).SetEase(Ease.OutBack))
                //.AppendCallback(() => card.transform.SetParent(containerPosition))
                .Append(card.transform.DOLocalMove(new Vector3(0, 0, 0), cardAnim * 0.3f).SetEase(Ease.InQuad));

                yield return putCardInHand.WaitForCompletion();

                //card.transform.DOLocalMove(new Vector3(0, 0, 0), cardAnim).SetEase(Ease.OutBack);
                //card.transform.DOScale(Vector3.one, cardAnim).SetEase(Ease.OutBack);

                //deckSize--;
            }
        }
    }

    public void ReadyCard(int index, bool ready)
    {
        if (Hand.transform.childCount > 0)
        {
            var firstCard = Hand.transform.GetChild(index).GetChild(0);

            if (ready == true)
            {
                firstCard.transform.DOScale(1.2f, 0.35f);
            }
            if (ready == false)
            {
                firstCard.transform.DOScale(1f, 0.35f);
            }
        }
    }

    public IEnumerator PlayCard()
    {
        //PlayArea.SetActive(false);


        if (cardsInHandCount > 0 && !drawIsRunning) 
        {
            GameObject readyCard = Hand.transform.GetChild(Hand.transform.childCount - 1 - cardsPlayed).GetChild(0).gameObject;
            GameObject readyCardContainer = Hand.transform.GetChild(Hand.transform.childCount - 1 - cardsPlayed).gameObject;
            CardUI readyCardUI = readyCard.GetComponent<CardUI>();
            int readyCardPriority = readyCardUI.priority;
            int readyCardGold = readyCardUI.gold;
            int readyCardMana = readyCardUI.mana;
            int readyCardDraws = readyCardUI.draws;
            //Debug.Log("draws = " + readyCardDraws);

            switch (readyCardPriority)
            {
                case 0:
                    priority0count--;
                    break;
                case 1:
                    priority1count--;
                    break;
                case 2:
                    priority2count--;
                    break;
                case 3:
                    priority3count--;
                    break;
                case 4:
                    priority4count--;
                    break;
            }

            if (!PauseMenu.autoPlay)
            {
                for (int i = cardsInHandCount; i-- > 0;)
                {
                    GameObject card = Hand.transform.GetChild(i).gameObject;

                    float duration = (float) cardPunch / cardsInHandCount;
                    if (i < 5)
                    {
                        duration = 0.15f;
                    }

                    //Tween punchTween = card.transform.DOPunchPosition(new Vector3(0, 50, 0), duration, 0, 0f);
                    //punchTween.Play();

                    card.transform.DOPunchPosition(new Vector3(0, 50, 0), duration, 0, 0f);

                    yield return new WaitForSeconds(0.05f);
                }
            }
 
            Sequence playTween = DOTween.Sequence();
            playTween.Join(readyCardContainer.GetComponent<LayoutElement>().DOPreferredSize(Vector2.zero, playCardContainerSpeed));
            playTween.Join(readyCard.transform.DOMoveY(playCardDrop, playCardDropSpeed));

            yield return playTween.WaitForPosition(playCardDropSpeed * 0.6f);

            //Object.Destroy(readyCardContainer);

            if (cardsPlayed == synergyLevel)
            {
                cardsPlayed++;
            }

            GameObject slot = slotManager.transform.GetChild(cardsPlayed).GetChild(0).gameObject;
            slot.SetActive(true);
            //int slotId = slot.transform.GetChild(cardsPlayed).GetSiblingIndex();
            Image slotImage = slot.GetComponent<Image>();
            Color slotColor = slotImage.color;
            Sequence slotFlash = DOTween.Sequence();
            slotFlash.Join(slotImage.DOColor(new Color(255f, 255f, 255f), 0.05f)).Append(slotImage.DOColor(slotColor, 0.1f));
            
            if(cardsPlayed == cardsDrawn)
            {
                yield return slotFlash.WaitForCompletion();
            }
            else
            {
                slotFlash.Play();
            }
            
            cardsPlayed++;

            cardsInHandCount--;

            if(readyCardUI.color == "Black")
            {
                if(currentHealth > 0)
                {
                    currentHealth--;
                }
            }
            if (readyCardUI.color == "White")
            {
                if(maxHealth != currentHealth)
                {
                    currentHealth++;
                }
            }
            healthBar.SetHealth(currentHealth);

            yield return new WaitForFixedUpdate();

            if (cardsInHandCount > 0 && readyCardDraws == 0)
            {
                ReadyCard(cardsInHandCount - 1, true);
            }

            yield return new WaitForFixedUpdate();

            if (readyCardDraws > 0 && cardsDrawn < deckSize)
            {
                queue.StopLoop();
                //Debug.Log("Queue Count = " + queue.GetCount());
                yield return StartCoroutine(DrawCards(readyCardDraws));
                if (PauseMenu.autoPlay)
                {
                    CheckQueue();
                }
                queue.StartLoop();
            }

            counters.AddGold(readyCardGold);

            counters.AddMana(readyCardMana);

            //Debug.Log("Queue Count = " + queue.GetCount());
            //PlayArea.SetActive(true);

            if (cardsDrawn > 1 && cardsInHandCount == 0)
            {
                //GameManager.OnGameStateChanged -= SetBattleState;
                if(cardsPlayed == synergyLevel)
                {
                    WinScreen.SetActive(true);
                    GameManager.Instance.UpdateGameState(GameState.Win);
                }
                DraftCanvas.SetActive(true);
            }
            
        }
    }

    public void ResetState()
    {
        if (GameManager.Instance.State == GameState.Battle && PauseMenu.autoPlay == false)
        {
            DeckButton.SetActive(true);
        }

        deckSize = deck.Count;

        Utilities.DeleteChildren(Hand.transform);

        priority0count = 0;
        priority1count = 0;
        priority2count = 0;
        priority3count = 0;
        priority4count = 0;

        cardsInHandCount = 0;
        cardsDrawn = 0;
        cardsPlayed = 0;

        slotManager.LoadSynergyBar();

        foreach (Transform slot in slotManager.transform)
        {
            slot.GetChild(0).gameObject.SetActive(false);
        }

        print("Player Reset");
    }

    public void CheckQueue()
    {
        int queueCount = queue.GetCount();
        //Debug.Log("Queue Count = " + queueCount);
        int cardCount = cardsInHandCount;
        //Debug.Log("Card Count = " + cardCount);

        if (cardCount > queueCount)
        {
            for (int i = cardCount; i >= queueCount; i--)
            {
                queue.EnqueueAction(PlayCard());
            }
        }
    }

    public void InvokeTest()
    {
        Debug.Log("working");
    }

    public void Shuffle()
    {
        for (int i = 0; i < deckSize; i++)
        {
            container[0] = deck[i];
            int randomIndex = Random.Range(i, deckSize);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = container[0];
        }
    }
}

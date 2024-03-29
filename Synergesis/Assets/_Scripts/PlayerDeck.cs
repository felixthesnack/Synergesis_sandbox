using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Linq;
using System;


public class PlayerDeck : MonoBehaviour
{
    public static PlayerDeck Instance;

    private List<Card> container = new List<Card>();
    //public int[] starterCards = { 0, 0, 1, 1, 3, 3, 4, 4, 2, 5 };
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
    //public CountersUI counters;

    public float cardAnim = 0.25f;
    public float handAnim = 0.75f;

    public float playCardDrop = -2f;
    public float playCardDropSpeed = 0.3f;
    public float playCardContainerSpeed = 0.15f;
    public float cardPunch = 0.1f;
    public bool drawIsRunning = false;

    public GameObject DrawDeck;

    public Card[] skillCards;
    public bool[] slotFull;
    public GameObject[] PlayerUISlots;

    Color blackColor = new Color32(0, 0, 0, 255);
    Color whiteColor = new Color32(255, 255, 255, 255);
    Color goldColor = new Color32(255, 206, 114, 255);
    Color greyColor = new Color32(135, 135, 135, 255);
    Color redColor = new Color32(200, 20, 20, 255);

    public GameObject Hand;
    public GameObject CardToHandContainer;
    public GameObject CardPlayed;
    public GameObject DeckView;
    public GameObject PlayArea;
    public GameObject DraftCanvas;
    public GameObject SkillCanvas;
    public GameObject DeckButton;
    public GameObject WinScreen; //create + LoseScreen

    [SerializeField] TMP_Text deckCountText;
    [SerializeField] TMP_Text cardsPlayedCountText;

    //[SerializeField] int attackCount = 0;

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
        Invoke("Initialize", 1);
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

    void Initialize()
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

        //foreach (var x in starterCards)
        //{
        //    starterDeck.Add(CardDatabase.cardList[x]);
        //}

        starterDeck.AddRange(CardDatabase.starterDeck);

        container.Add(new Card());

        deck.AddRange(starterDeck);
        deckSize = deck.Count;

        Invoke(iTest, 0);

        slotManager.LoadSynergyBar();

        maxHealth = 100; //(int)(Mathf.Round(((27 + (synergyLevel - 6) * (synergyLevel + 7) / 2) / 25) * 25) / 2);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth);

        deckCountText.text = deckSize.ToString();
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
        Debug.Log("draw is running " + drawIsRunning);
        yield return StartCoroutine(DrawCard(cards));

        // int cardIndex = containerPosition.GetSiblingIndex();
        //Debug.Log("card index = " + containerIndex);
        if (!PauseMenu.autoPlay)
        {
            if (cardsInHandCount > 1)
            {
                if (containerIndex == cardsInHandCount - 1)
                {
                    ReadyCard(containerIndex - 1, false);
                }
                //else { }
            }
            ReadyCard(cardsInHandCount - 1, true);
        }
        drawIsRunning = false;
        Debug.Log("draw is running " + drawIsRunning);
    }

    IEnumerator DrawCard(int cards)
    {
        if (cardsDrawn < deckSize)
        {
            for (int i = 0; i < cards; i++)

            {
                if((deckSize - cardsDrawn) > 0)
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
                else
                {
                    break;
                }
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


        if (cardsInHandCount > 0) 
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

            //if (cardsPlayed == synergyLevel)
            //{
            //    cardsPlayed++;
            //}

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

            cardsPlayedCountText.text = cardsPlayed.ToString();
            
            cardsInHandCount--;
            
            //healthBar.SetHealth(currentHealth);

            yield return new WaitForFixedUpdate();

            if (readyCardDraws > 0 && cardsDrawn < deckSize)
            {
                Debug.Log("Queue Count = " + queue.GetCount());
                //if (queue.GetCount() > 0)
                //{
                //    queue.PauseLoop();
                //}
                yield return StartCoroutine(DrawCards(readyCardDraws));
                //yield return new WaitForFixedUpdate();
                if (PauseMenu.autoPlay == true)
                {
                    CheckQueue();
                }
                //queue.StartLoop();
            }
            
            yield return new WaitForFixedUpdate();

            if (cardsInHandCount > 0 && readyCardDraws == 0 && !PauseMenu.autoPlay)
            {
                ReadyCard(cardsInHandCount - 1, true);
            }

            CountersUI.Instance.AddGold(readyCardGold);

            CountersUI.Instance.AddMana(readyCardMana);
            
            CheckSkillSlots(readyCardUI);

            yield return new WaitForFixedUpdate();

            //Debug.Log("Queue Count = " + queue.GetCount());
            //PlayArea.SetActive(true);

            if (cardsDrawn > 1 && cardsInHandCount == 0)
            {
                //GameManager.OnGameStateChanged -= SetBattleState;
                //if(cardsPlayed == synergyLevel)
                //{
                //    WinScreen.SetActive(true);
                //    GameManager.Instance.UpdateGameState(GameState.Win);
                //}
                var min = Mathf.Infinity;
                
                if (slotFull[0] == true)
                {

                    for (int i = 0; i < slotFull.Length; i++)
                    {
                        if (slotFull[i] == true)
                        {
                            if (skillCards[i].manaCost < min)
                            {
                                min = skillCards[i].manaCost;
                            }
                        }
                    }
                    //GameManager.Instance.UpdateGameState(GameState.Skill);
                }
                if(CountersUI.Instance.currentMana >= min)
                {
                    SkillCanvas.SetActive(true);
                }
                else
                {
                    DraftCanvas.SetActive(true);
                }
            }

        }
    }
    public void CheckSkillSlots(CardUI cardUI)
    {
        if (cardUI.type == "tier1skill")
        {
            for (int i = 0; i < PlayerUISlots.Length; i++)
            {
                if (slotFull[i] == false)
                {
                    slotFull[i] = true;
                    skillCards[i] = cardUI.cardData;

                    Image slotBorderColor = PlayerUISlots[i].GetComponent<Image>();
                    Image slotColor = PlayerUISlots[i].transform.GetChild(0).GetComponent<Image>();
                    slotBorderColor.color = redColor;
                    switch (cardUI.color)
                    {
                        case "Black":
                            slotColor.color = blackColor;
                            break;
                        case "White":
                            slotColor.color = whiteColor;
                            break;
                        case "Gold":
                            slotColor.color = goldColor;
                            break;
                        case "Colorless":
                            slotColor.color = greyColor;
                            break;
                    }
                    break;
                }

            }
        }
    }

    public void ResetState()
    {
        Array.Clear(slotFull, 0, slotFull.Length);
        Array.Clear(skillCards, 0, skillCards.Length);


        for (int i = 0; i < PlayerUISlots.Length; i++)
        {
            Image slotBorderColor = PlayerUISlots[i].GetComponent<Image>();
            Image slotColor = PlayerUISlots[i].transform.GetChild(0).GetComponent<Image>();
            slotBorderColor.color = blackColor;
            slotColor.color = greyColor;
        }

        if (GameManager.Instance.State == GameState.Battle && PauseMenu.autoPlay == false)
        {
            DeckButton.SetActive(true);
        } 
        
        else
        {
            DeckButton.SetActive(false);
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

        deckCountText.text = deckSize.ToString();
        cardsPlayedCountText.text = cardsPlayed.ToString();

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
            int randomIndex = UnityEngine.Random.Range(i, deckSize);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = container[0];
        }
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DraftScreenManager : MonoBehaviour
{
    public static DraftScreenManager Instance;

    public int trashCost = 10;

    public GameObject LeftCard;
    public GameObject RightCard;
    public GameObject ChooseText;
    public GameObject BottomText;
    public GameObject ZoomPanel;
    private CardUI LeftCardUI;
    private CardUI RightCardUI;
    private Button LeftCardButton;
    private Button RightCardButton;
    private Button BottomTextButton;
    [SerializeField] private TMP_Text BottomTextText;

    [SerializeField] GameObject TearPrefab;

    [SerializeField] float animSpeed = 0.75f;

    private void Awake()
    {
        Instance = this;
        LeftCardUI = LeftCard.GetComponent<CardUI>();
        RightCardUI = RightCard.GetComponent<CardUI>();
        LeftCardButton = LeftCard.GetComponent<Button>();
        RightCardButton = RightCard.GetComponent<Button>();
        BottomTextButton = BottomText.GetComponentInChildren<Button>();
    }

    void OnEnable()
    {
        GameManager.Instance.UpdateGameState(GameState.Draft);
        ResetCanvas();
        Card leftCard = CardDatabase.Instance.DrawTier1Card();
        Card rightCard = CardDatabase.Instance.DrawTier1Card();

        while (leftCard.id == rightCard.id)
        {
            rightCard = CardDatabase.cardDatabase[Random.Range(6, 40)];
        }

        LeftCardUI.LoadCard(leftCard);
        RightCardUI.LoadCard(rightCard);

        LeftCardButton.interactable = false;
        RightCardButton.interactable = false;
        BottomTextButton.interactable = false;
            
        StartCoroutine(TweenUIBegin());

    }

    public IEnumerator TweenUIBegin()
    {
        Sequence tweenUIBegin = DOTween.Sequence();
        //Sequence endPunch = DOTween.Sequence();
        //endPunch.Join(LeftCard.transform.DOPunchRotation(new Vector3(0f, 0f, -7.5f), 0.25f, 10, 4f))
        //    .Join(RightCard.transform.DOPunchRotation(new Vector3(0f, 0f, 7.5f), 0.25f, 10, 5f));
        tweenUIBegin.Join(LeftCard.transform.DOLocalMoveX(-280f, animSpeed))
            .Join(LeftCard.transform.DOLocalRotate(Vector3.zero, animSpeed))
            .Join(RightCard.transform.DOLocalMoveX(280f, animSpeed))
            .Join(RightCard.transform.DOLocalRotate(Vector3.zero, animSpeed))
            .Join(ChooseText.transform.DOLocalMoveY(79f, animSpeed))
            .Join(BottomText.transform.DOLocalMoveY(-407.5f, animSpeed));
        //.Append(endPunch);
        if (GameManager.Instance.State == GameState.Draft && CountersUI.Instance.currentGold >= trashCost && PlayerDeck.Instance.deckSize > 10)
        {
            BottomTextText.SetText("EDIT DECK");
        }
        else
        {
            BottomTextText.SetText("VIEW DECK");
        }
        
        yield return tweenUIBegin.WaitForCompletion();
        LeftCardButton.interactable = true;
        RightCardButton.interactable = true;
        BottomTextButton.interactable = true;
    }

    public IEnumerator TweenUIEnd()
    {
        Sequence tweenUIEnd = DOTween.Sequence();

        tweenUIEnd.Join(ChooseText.transform.DOLocalMoveY(870f, animSpeed))
            .Join(BottomText.transform.DOLocalMoveY(-685f, animSpeed));

        yield return tweenUIEnd.WaitForCompletion();
    }

    public void ChooseRightCard()
    {
        PlayerDeck.Instance.deck.Add(CardDatabase.cardDatabase[RightCardUI.id]);
        PlayerDeck.Instance.deckSize = PlayerDeck.Instance.deck.Count;

        GameObject TearAnim = Instantiate(TearPrefab, this.gameObject.transform);
        TearAnim.transform.localPosition = new Vector3(-272.5f, 85.75f, 1);
        TearAnim.transform.localScale = new Vector3(3.225f, 3.75f, 1);
        Animator anim = TearAnim.GetComponent<Animator>();

        StartCoroutine(DisableCard(LeftCard));
        RightCard.transform.DOLocalMoveY(-1060f, 0.5f);
        
        IEnumerator wait()
        {
            while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < anim.GetCurrentAnimatorStateInfo(0).length)
            { 
                yield return null; 
            
            }

            GameManager.Instance.UpdateGameState(GameState.Battle);
            yield return StartCoroutine(TweenUIEnd());
            Destroy(TearAnim.gameObject);
            gameObject.SetActive(false);
        }

        StartCoroutine(wait());

    }

    public void ChooseLeftCard()
    {
        PlayerDeck.Instance.deck.Add(CardDatabase.cardDatabase[LeftCardUI.id]);
        PlayerDeck.Instance.deckSize = PlayerDeck.Instance.deck.Count;

        GameObject TearAnim = Instantiate(TearPrefab, this.gameObject.transform);
        TearAnim.transform.localPosition = new Vector3(283.5f, 85.75f, 1);
        TearAnim.transform.localScale = new Vector3(3.225f, 3.75f, 1);
        Animator anim = TearAnim.GetComponent<Animator>();

        StartCoroutine(DisableCard(RightCard));
        LeftCard.transform.DOLocalMoveY(-1060f, 0.5f);

        IEnumerator wait()
        {
            while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < anim.GetCurrentAnimatorStateInfo(0).length)
            {
                yield return null;

            }

            GameManager.Instance.UpdateGameState(GameState.Battle);
            yield return StartCoroutine(TweenUIEnd());
            Destroy(TearAnim.gameObject);
            gameObject.SetActive(false);
        }

        StartCoroutine(wait());

    }

    IEnumerator DisableCard(GameObject card)
    {
        yield return new WaitForSeconds(0.1f);
        card.SetActive(false);
    }

    public void ResetCanvas()
    {
        if (!LeftCard.activeSelf)
        {
            LeftCard.SetActive(true);
        }
        if (!RightCard.activeSelf)
        {
            RightCard.SetActive(true);
        }

        if (ZoomPanel.activeSelf)
        {
            ZoomPanel.SetActive(false);
        }

        BottomText.SetActive(true);

        Vector3 leftReset = new Vector3(-1100f, -185f, 0f);
        Vector3 rightReset = new Vector3(1100f, -185f, 0f);

        LeftCard.transform.SetParent(gameObject.transform);
        LeftCard.transform.localPosition = leftReset;
        LeftCard.transform.localEulerAngles = new Vector3(0, 0, 20);

        RightCard.transform.SetParent(gameObject.transform);
        RightCard.transform.localPosition = rightReset;
        RightCard.transform.localEulerAngles = new Vector3(0, 0, -20);

        ChooseText.transform.localPosition = new Vector3(0, 870, 0);
        BottomText.transform.localPosition = new Vector3(0, -685, 0);
    }
}

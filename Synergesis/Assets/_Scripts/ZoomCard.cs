using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class ZoomCard : MonoBehaviour
{
    private GameObject ZoomPanel;
    private GameObject DeckView;
    private Button trashButton;
    [SerializeField] GameObject TearPrefab;

    //private GameObject parentContainer;

    private DeckCardUI deckCardUI;

    private Image panelImage;

    [SerializeField] float zoomSpeed = 0.25f;
    private Vector3 zoomScale, normalScale, zoomPosition, normalPosition;
    private bool zoomed;

    public int parentIndex;


    void Start()
    {
        DeckView = DeckUI.Instance.DeckUIContainer;
        ZoomPanel = DeckUI.Instance.ZoomPanel;
        panelImage = ZoomPanel.GetComponent<Image>();
        trashButton = transform.GetChild(2).GetComponent<Button>();

        deckCardUI = GetComponent<DeckCardUI>();

        float midX = (float)(0 - 100 * 1.5);
        float midY = (float)(0 + 150 * 1.5);

        //parentContainer = transform.parent.gameObject;
        //parentIndex = parentContainer.transform.GetSiblingIndex();
        
        trashButton.enabled = false;

        zoomScale = new Vector3(1.5f, 1.5f, 0f);
        zoomPosition = new Vector3(midX, midY, 0f);
        zoomed = false;
    }

    //private void OnEnable()
    //{
    //    parentContainer = transform.parent.gameObject;
    //    parentIndex = parentContainer.transform.GetSiblingIndex();
    //}

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = new Vector3(1.2f, 1.2f, 1f);
        deckCardUI.TrashText.SetActive(false);
    }

    public void DoZoomCard()
    {
        if (!zoomed)
        {
            StartCoroutine(zoomIn());
            zoomed = !zoomed;
        }
        else
        {
            StartCoroutine(zoomOut());
            zoomed = !zoomed;
        }
    }

    private IEnumerator zoomIn()
    {
        //parentContainer = transform.parent.gameObject;
        //parentIndex = parentContainer.transform.GetSiblingIndex();

        ZoomPanel.SetActive(true);
        transform.SetParent(ZoomPanel.transform);

        yield return new WaitForFixedUpdate();

        normalScale = transform.localScale;
        normalPosition = transform.localPosition;

        panelImage.DOFade(0.5f, 0.5f);

        Sequence zoomIn = DOTween.Sequence();
        zoomIn.Join(transform.DOLocalMove(zoomPosition, zoomSpeed)).Join(transform.DOScale(zoomScale, zoomSpeed));
        yield return zoomIn.WaitForCompletion();
        if (CountersUI.Instance.currentGold >= deckCardUI.trashCost && GameManager.Instance.State == GameState.Draft)
        {
            deckCardUI.TrashText.SetActive(true);
            trashButton.enabled = true;
        }
    }

    private IEnumerator zoomOut()
    {
        panelImage.DOFade(0, 0.5f);
        
        deckCardUI.TrashText.SetActive(false);
        trashButton.enabled = false;

        Sequence zoomOut = DOTween.Sequence();
        zoomOut.Join(transform.DOLocalMove(normalPosition, zoomSpeed)).Join(transform.DOScale(normalScale, zoomSpeed));
        yield return zoomOut.WaitForCompletion();

        transform.SetParent(DeckView.transform.GetChild(parentIndex).transform);

        yield return new WaitForFixedUpdate();

        transform.localScale = new Vector3(1.2f, 1.2f, 0f);
        transform.localPosition = Vector3.zero;
        
        ZoomPanel.SetActive(false);
    }

    public void TrashCard()
    {
        GameObject card = gameObject;
        StartCoroutine(trashCard(card));
       
        IEnumerator trashCard(GameObject card)
        {
            GameObject TearAnim = Instantiate(TearPrefab, card.gameObject.transform);
            TearAnim.transform.localPosition = new Vector3(101.5f, -145.285f, 1);
            TearAnim.transform.localScale = new Vector3(2.25f, 2.503f, 1);
            Animator anim = TearAnim.GetComponent<Animator>();

            DeckCardUI deckCardUI = card.GetComponent<DeckCardUI>();

            Button zoomOutButtonInvoke = card.GetComponent<Button>();

            string color = deckCardUI.color;
            switch (color)
            {
                case "Black":
                    DeckUI.Instance.blackCount--;
                    DeckUI.Instance.blackCountText.text = DeckUI.Instance.blackCount.ToString();
                    break;
                case "White":
                    DeckUI.Instance.whiteCount--;
                    DeckUI.Instance.whiteCountText.text = DeckUI.Instance.whiteCount.ToString();
                    break;
                case "Gold":
                    DeckUI.Instance.goldCount--;
                    DeckUI.Instance.goldCountText.text = DeckUI.Instance.goldCount.ToString();
                    break;
                case "Colorless":
                    DeckUI.Instance.colorlessCount--;
                    DeckUI.Instance.colorlessCountText.text = DeckUI.Instance.colorlessCount.ToString();
                    break;
            }

            if (deckCardUI.counter == 1)
            {
                int id = deckCardUI.id;
                int xIndex = PlayerDeck.Instance.deck.FindIndex(x => x.id == id);
                PlayerDeck.Instance.deck.Remove(PlayerDeck.Instance.deck[xIndex]);
                PlayerDeck.Instance.slotManager.RemoveSlot();

                TearAnim.transform.SetParent(ZoomPanel.transform);

                yield return new WaitForSeconds(0.1f);
                card.transform.localScale = Vector3.zero;

                while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < anim.GetCurrentAnimatorStateInfo(0).length)
                {
                    yield return null;
                    Console.WriteLine("while loop");
                }

                //yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length - 0.1f);

                transform.SetParent(DeckView.transform.GetChild(parentIndex).transform);
                yield return new WaitForFixedUpdate();

                panelImage.DOFade(0, 0.5f);
                zoomed = false;
                ZoomPanel.SetActive(false);

                //Destroy(parentContainer);
                Destroy(TearAnim);
                DraftScreenManager.Instance.gameObject.SetActive(false);
                ButtonController.Instance.Back();
                GameManager.Instance.UpdateGameState(GameState.Battle);
                //transform.SetParent(DeckView.transform.GetChild(parentIndex).transform);
                //transform.localPosition = Vector3.zero;
                //deckCardUI.TrashText.SetActive(false);
                //card.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
                //yield return new WaitForSeconds(2f);
            }
            if (deckCardUI.counter > 1)
            {
                deckCardUI.counter--;
                deckCardUI.CountNumber.text = deckCardUI.counter.ToString();

                int id = deckCardUI.id;
                int xIndex = PlayerDeck.Instance.deck.FindIndex(x => x.id == id);
                PlayerDeck.Instance.deck.Remove(PlayerDeck.Instance.deck[xIndex]);
                PlayerDeck.Instance.slotManager.RemoveSlot();

                while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < anim.GetCurrentAnimatorStateInfo(0).length)
                {
                    yield return null;
                }
                zoomOutButtonInvoke.onClick.Invoke();

                Destroy(TearAnim);
                DraftScreenManager.Instance.gameObject.SetActive(false);
                ButtonController.Instance.Back();
                GameManager.Instance.UpdateGameState(GameState.Battle);
            }
        }

        CountersUI.Instance.SpendGold(DraftScreenManager.Instance.trashCost);
    }
}

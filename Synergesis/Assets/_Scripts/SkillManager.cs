using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.UI;


public class SkillManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SkillManager Instance;

    //public Card[] skillCards;
    //public bool[] slotFull;
    //public GameObject[] PlayerUISlots;
    public GameObject[] SkillCardSlots;
    public GameObject ContinueButtons;
    public GameObject SkipButton;
    public GameObject ConfirmButton;

    //public GameObject DraftCanvas;

    [SerializeField]
    private GameObject SkillScreenText;

    [SerializeField]
    private float tweenSpeed = 0.4f;

    [SerializeField]
    private float skillCardSlotY = -40f;

    public int spendManaCostValue = 0;
    public GameObject SpendManaCost;
    public TMP_Text spendManaCostText;

    public int selectedCards = 0;

    //Color blackColor = new Color32(0, 0, 0, 255);
    //Color whiteColor = new Color32(255, 255, 255, 255);
    //Color goldColor = new Color32(255, 206, 114, 255);
    //Color greyColor = new Color32(135, 135, 135, 255);
    //Color redColor = new Color32(200, 20, 20, 255);

    private void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        Debug.Log(GameManager.Instance.name);
        Debug.Log(GameManager.Instance.State);

        GameManager.Instance.UpdateGameState(GameState.Skill);
        ResetSkillScreen();

        for (int i = 0; i < SkillCardSlots.Length; i++)
        {
            Button SkillCardButton = SkillCardSlots[i].transform.GetChild(2).GetComponent<Button>();
            SkillCardButton.interactable = false;
        }

        StartCoroutine(LoadSkillScreen());
    }

    //public void CheckSkillSlots(CardUI cardUI)
    //{
    //    if (cardUI.type == "tier1skill")
    //    {
    //        for (int i = 0; i < PlayerUISlots.Length; i++)
    //        {
    //            if (slotFull[i] == false)
    //            {
    //                slotFull[i] = true;
    //                skillCards[i] = cardUI.cardData;

    //                Image slotBorderColor = PlayerUISlots[i].GetComponent<Image>();
    //                Image slotColor = PlayerUISlots[i].transform.GetChild(0).GetComponent<Image>();
    //                slotBorderColor.color = redColor;
    //                switch (cardUI.color)
    //                {
    //                    case "Black":
    //                        slotColor.color = blackColor;
    //                        break;
    //                    case "White":
    //                        slotColor.color = whiteColor;
    //                        break;
    //                    case "Gold":
    //                        slotColor.color = goldColor;
    //                        break;
    //                    case "Colorless":
    //                        slotColor.color = greyColor;
    //                        break;
    //                }
    //                break;
    //            }

    //        }
    //    }
    //}

    //public void ResetPlayerSkillUI()
    //{
    //    Array.Clear(slotFull, 0, slotFull.Length);
    //    Array.Clear(skillCards, 0, skillCards.Length);


    //    for (int i = 0; i < PlayerUISlots.Length; i++)
    //    {
    //        Image slotBorderColor = PlayerUISlots[i].GetComponent<Image>();
    //        Image slotColor = PlayerUISlots[i].transform.GetChild(0).GetComponent<Image>();
    //        slotBorderColor.color = blackColor;
    //        slotColor.color = greyColor;
    //    }
    //}

    public void ResetSkillScreen()
    {
        int resetY = -860;

        for (int i = 0; i < SkillCardSlots.Length; i++)
        {
            Vector3 skillPosition = SkillCardSlots[i].transform.localPosition;
            skillPosition.y = resetY;
            //SelectCard selectedCard = SkillCardSlots[i].transform.GetChild(2).GetComponent<SelectCard>();
            //selectedCard.selected = false;
        }

        Vector3 skillScreenTextPosition = new Vector3(0, 605, 0);
        Vector3 continueButtonsPosition = new Vector3(765, -600, 0);

        SkillScreenText.transform.localPosition = skillScreenTextPosition;
        ContinueButtons.transform.localPosition = continueButtonsPosition;
        spendManaCostValue = 0;
        SpendManaCost.SetActive(false);
    }

    public IEnumerator LoadSkillScreen()
    {
        float delay = 0f;
        
        Sequence animateSlots = DOTween.Sequence();

        for (int i = 0; i < SkillCardSlots.Length; i++)
        {
            GameObject SkillCard = SkillCardSlots[i].transform.GetChild(2).gameObject;

            if (PlayerDeck.Instance.slotFull[i] == true)
            {
                SkillCard.SetActive(true);
            }
            else
            {
                SkillCard.SetActive(false);
            }
            CardUI SkillCardSlotUI = SkillCard.GetComponent<CardUI>();

            SkillCardSlotUI.LoadCard(PlayerDeck.Instance.skillCards[i]);

            animateSlots.Join(SkillCardSlots[i].transform.DOLocalMoveY(skillCardSlotY, tweenSpeed).SetDelay(delay).SetEase(Ease.OutBounce));
            delay += 0.2f;
        }

        animateSlots.Join(SkillScreenText.transform.DOLocalMoveY(375f, animateSlots.Duration()))
            .Join(ContinueButtons.transform.DOLocalMoveY(-430f, animateSlots.Duration()));
        
        yield return animateSlots.WaitForCompletion();

        for (int i = 0; i < SkillCardSlots.Length; i++)
        {
            Button SkillCardButton = SkillCardSlots[i].transform.GetChild(2).GetComponent<Button>();
            SkillCardButton.interactable = true;
        }
    }


    public IEnumerator ExitSkillScreen()
    {
        Sequence animateSlotsExit = DOTween.Sequence();

        for (int i = 0; i < SkillCardSlots.Length; i++)
        {
            GameObject SkillCard = SkillCardSlots[i].transform.GetChild(2).gameObject;
            SelectCard selectedCard = SkillCard.GetComponent<SelectCard>();
            Button SkillCardButton = SkillCard.GetComponent<Button>();
            SkillCardButton.interactable = false;
            
            if (selectedCard.selected == true)
            {
                animateSlotsExit.Join(SkillCardSlots[i].transform.DOLocalMoveY(SkillCardSlots[i].transform.localPosition.y + 900f, tweenSpeed).SetEase(Ease.InQuad));
                selectedCard.SelectToggle();
            }
            else
            {
                animateSlotsExit.Join(SkillCardSlots[i].transform.DOLocalMoveY(SkillCardSlots[i].transform.localPosition.y - 820f, tweenSpeed).SetEase(Ease.InQuad));
            }
        }

        animateSlotsExit.Join(SkillScreenText.transform.DOLocalMoveY(SkillScreenText.transform.localPosition.y + 900f, animateSlotsExit.Duration()))
            .Join(ContinueButtons.transform.DOLocalMoveY(ContinueButtons.transform.localPosition.y - 170f, animateSlotsExit.Duration()));

        yield return animateSlotsExit.WaitForCompletion();
    }

    public void ConfirmChoice()
    {
        CountersUI.Instance.SpendMana(spendManaCostValue);
        StartCoroutine(wait());
    }

    public void SkipChoice()
    {

        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return StartCoroutine(ExitSkillScreen());
        DraftScreenManager.Instance.gameObject.SetActive(true);
       // GameManager.Instance.UpdateGameState(GameState.Draft);
    }

    public void UpdateManaText()
    {
        spendManaCostText.text = spendManaCostValue.ToString();
    }
}


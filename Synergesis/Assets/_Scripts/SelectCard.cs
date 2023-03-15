using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCard : MonoBehaviour
{
    public bool selected = false;
    public CardUI thisCard;

    [SerializeField]
    private GameObject mainShadow,  priorityShadow;
    private Image mainShadowImage, priorityShadowImage;
    private Vector3 mainShadowPosition, priorityShadowPosition;
    private RectTransform mainShadowRect, priorityShadowRect;
    private Vector2 mainShadowSize, priorityShadowSize;
    private Color shadow = new Color32(0, 0, 0, 75);
    private Color glow = new Color32(125, 255, 245, 255);

    private void Awake()
    {
        thisCard = gameObject.GetComponent<CardUI>();
        mainShadowImage = mainShadow.transform.GetComponent<Image>();
        priorityShadowImage = priorityShadow.transform.GetComponent<Image>();
        mainShadowRect = mainShadow.transform.GetComponent<RectTransform>();
        priorityShadowRect = priorityShadow.transform.GetComponent<RectTransform>();
        mainShadowPosition = new Vector3(5, -6, 0);
        priorityShadowPosition = new Vector3(88, 138, 0);
        mainShadowSize = mainShadowRect.sizeDelta;
        priorityShadowSize = priorityShadowRect.sizeDelta;
    }

    public void SelectToggle()
    {
        if (!selected)
        {
            SelectThisCard();
            selected = !selected;
        }
        else
        {
            DeselectThisCard();
            selected = !selected;
        }
    }
    public void SelectThisCard()
    {
        if(CountersUI.Instance.currentMana >= thisCard.manaCost)
        {
            mainShadowRect.localPosition = Vector3.zero;
            mainShadowRect.sizeDelta = new Vector2(210, 310);
            mainShadowImage.color = glow;

            priorityShadowRect.localPosition = new Vector3(85, 140, 0);
            priorityShadowRect.sizeDelta = new Vector2(58, 58);
            priorityShadowImage.color = glow;

            SkillManager.Instance.selectedCards++;

            if(SkillManager.Instance.selectedCards > 0)
            {
                SkillManager.Instance.SkipButton.SetActive(false);
                SkillManager.Instance.ConfirmButton.SetActive(true);
            }
        
            SkillManager.Instance.SpendManaCost.SetActive(true);
            SkillManager.Instance.spendManaCostValue += thisCard.manaCost;
            SkillManager.Instance.UpdateManaText();
        }
    }
    public void DeselectThisCard()
    {
        mainShadow.transform.localPosition = mainShadowPosition;
        mainShadowRect.sizeDelta = mainShadowSize;
        mainShadowImage.color = shadow;

        priorityShadow.transform.localPosition = priorityShadowPosition;
        priorityShadowRect.sizeDelta = priorityShadowSize;
        priorityShadowImage.color = shadow;

        SkillManager.Instance.selectedCards--;

        if (SkillManager.Instance.selectedCards == 0)
        {
            SkillManager.Instance.ConfirmButton.SetActive(false);
            SkillManager.Instance.SkipButton.SetActive(true);
        }

        SkillManager.Instance.spendManaCostValue -= thisCard.manaCost;

        if(SkillManager.Instance.spendManaCostValue <= 0)
        {
            SkillManager.Instance.SpendManaCost.SetActive(false);
        }
        else
        {
             SkillManager.Instance.UpdateManaText();
        }
    }
}

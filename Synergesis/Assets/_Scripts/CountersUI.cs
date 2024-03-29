using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountersUI : MonoBehaviour
{
    public static CountersUI Instance;

    public int currentGold = 0;
    public int currentMana = 0;
    public int turnCounter = 0;

    [SerializeField] TMP_Text goldText;
    [SerializeField] TMP_Text manaText;
    [SerializeField] TMP_Text turnCounterText;

    private void Awake()
    {
        Instance = this;
    }
    public void UpdateTurn()
    {
        turnCounter++;
        turnCounterText.text = turnCounter.ToString();
    }

    public void AddGold(int gold)
    {
        currentGold += gold;
        goldText.text = currentGold.ToString();
    }

    public void SpendGold(int gold)
    {
        if (currentGold >= 0)
        {
            currentGold -= gold;
            Mathf.Clamp(currentGold, 0, Mathf.Infinity);
        }
        goldText.text = currentGold.ToString();
    }

    public void AddMana(int mana)
    {
        currentMana += mana;
        manaText.text = currentMana.ToString();
    }

    public void SpendMana(int mana)
    {
        if (currentMana >= 0)
        {
            currentMana -= mana;
            Mathf.Clamp(currentMana, 0, Mathf.Infinity);
        }
        manaText.text = currentMana.ToString();
    }
}

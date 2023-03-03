using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class CardDatabase : MonoBehaviour
{
    public static CardDatabase Instance;

    static CardList cardList = new CardList();

    //static JSON_test jsonCards;

    public static List<Card> cardDatabase = new List<Card>();

    public static List<Card> starterDeck = new List<Card>();
    private int[] starterCards = { 1, 1, 2, 2, 4, 4, 5, 5, 3, 6 };

    [SerializeField]
    public static List<Card> tier1Starter = new List<Card>();
    public static List<Card> tier1Basic = new List<Card>();
    public static List<Card> tier1Special = new List<Card>();
    public static List<Card> tier1Skills = new List<Card>();

    public static int tier1BasicChance = 50;
    public static int tier1SpecialChance = 30;
    public static int tier1SkillChance = 20;

    public static int tier1CardDrawRatio = tier1BasicChance + tier1SpecialChance + tier1SkillChance;

    void Awake()
    {

        Instance = this;

        StartCoroutine(GetJSONCardList());

    }

    IEnumerator GetJSONCardList()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://opensheet.elk.sh/10vYtq-q9gdNFNhbog2LU1W2QTe42-c1xuMkZJOh1kIk/Sheet2");
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
            cardList = JsonUtility.FromJson<CardList>("{\"JSONCards\":" + json + "}");
            Debug.Log(cardList.JSONCards.Count);
            Debug.Log(cardList.JSONCards[6].cardName);

            for (int i = 1; i < 7; i++)
            {
                tier1Starter.Add(cardList.JSONCards[i]);
                Debug.Log("Added " + cardList.JSONCards[i].id + " card");
            }

            for (int i = 7; i < 16; i++)
            {
                tier1Basic.Add(cardList.JSONCards[i]);
                Debug.Log("Added " + cardList.JSONCards[i].id + " card");
            }

            for (int i = 16; i < 32; i++)
            {
                tier1Special.Add(cardList.JSONCards[i]);
                Debug.Log("Added " + cardList.JSONCards[i].id + " card");
            }

            for (int i = 32; i < 41; i++)
            {
                tier1Skills.Add(cardList.JSONCards[i]);
                Debug.Log("Added " + cardList.JSONCards[i].id + " card");
            }

            cardDatabase.Add(cardList.JSONCards[0]);
            cardDatabase.AddRange(tier1Starter);
            cardDatabase.AddRange(tier1Basic);
            cardDatabase.AddRange(tier1Special);
            cardDatabase.AddRange(tier1Skills);

            foreach (var x in starterCards)
            {
                starterDeck.Add(cardDatabase[x]);
            }
        }
    }

    public Card DrawTier1Card()
    {
        Card tier1Card = new Card();

        int chance = Random.Range(1, tier1CardDrawRatio + 1);


        if ((chance -= tier1SkillChance) < 0)
        {
            tier1Card = CardDatabase.tier1Skills[Random.Range(0, tier1Skills.Count)];
        }
        else if ((chance -= tier1SpecialChance) < 0)
        {
            tier1Card = CardDatabase.tier1Special[Random.Range(0, tier1Special.Count)];
        }
        else
        {
            tier1Card = CardDatabase.tier1Basic[Random.Range(0, tier1Basic.Count)];
        }

        return tier1Card;
    }
}

public class CardList
{
    public List<Card> JSONCards;
}

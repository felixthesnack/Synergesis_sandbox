using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JSON_test : MonoBehaviour
{

    public CardList cardList = new CardList();
    // Start is called before the first frame update
    //void Awake()
    //{
    //    StartCoroutine(GetJSONCardList());

    //    //for (int i = 0; i < 6; i++)
    //    //{
    //    //    CardDatabase.tier1Starter.Add(cardList.JSONCards[i]);
    //    //    Debug.Log("Added " + cardList.JSONCards[0].cardName + " card");
    //    //}
    //}

    //IEnumerator GetJSONCardList()
    //{
    //    UnityWebRequest www = UnityWebRequest.Get("https://opensheet.elk.sh/10vYtq-q9gdNFNhbog2LU1W2QTe42-c1xuMkZJOh1kIk/Sheet2");
    //    yield return www.SendWebRequest();
    //    if(www.result == UnityWebRequest.Result.ProtocolError)
    //    {
    //        Debug.Log("Error: " + www.error);
    //    }
    //    else
    //    {
    //        string json = www.downloadHandler.text;
    //        cardList = JsonUtility.FromJson<CardList>("{\"JSONCards\":" + json + "}");
    //        Debug.Log(cardList.JSONCards.Count);
    //        Debug.Log(cardList.JSONCards[6].cardName);

    //        for (int i = 0; i < 6; i++)
    //        {
    //            CardDatabase.tier1Starter.Add(cardList.JSONCards[i]);
    //            Debug.Log("Added " + cardList.JSONCards[i].id + " card");
    //        }
    //    }
    //}

}

//[Serializable]
//public class CardList
//{
//    public List<Card> JSONCards;
//}

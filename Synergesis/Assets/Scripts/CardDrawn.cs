using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject DrawDeck;
    public GameObject HandCard;
    // Start is called before the first frame update
    void Awake()
    {
        DrawDeck = GameObject.Find("DrawDeck");
        HandCard.transform.SetParent(DrawDeck.transform);
        HandCard.transform.localScale = Vector3.one;
        HandCard.transform.position = DrawDeck.transform.position;
        HandCard.transform.eulerAngles = new Vector3(25, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //DrawDeck = GameObject.Find("DrawDeck");
        //HandCard.transform.SetParent(DrawDeck.transform);
        //HandCard.transform.localScale = Vector3.one;
        //HandCard.transform.position = DrawDeck.transform.position;
        //HandCard.transform.eulerAngles = new Vector3(25, 0, 0);
    }
}

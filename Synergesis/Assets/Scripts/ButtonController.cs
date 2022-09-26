using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject HandParent;
    [SerializeField] PlayerDeck playerDeck;


    public void ResetScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Utilities.DeleteChildren(HandParent.transform);

        playerDeck.deck = playerDeck.starterDeck;
        playerDeck.deckSize = playerDeck.deck.Count;

        playerDeck.priority1count = 0;
        playerDeck.priority2count = 0;
        playerDeck.priority3count = 0;

        playerDeck.cardsInHand.Clear();

        StartCoroutine(playerDeck.StartTurn());



        print("Game Reset");
    }
}


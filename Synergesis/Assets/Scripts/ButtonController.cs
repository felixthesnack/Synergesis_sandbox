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

        for (var i = HandParent.transform.childCount -1; i >= 0; i--)
        {
            Object.Destroy(HandParent.transform.GetChild(i).gameObject);
        }

        playerDeck.deck = playerDeck.starterDeck;
        playerDeck.cardsInHand.Clear();

        playerDeck.Shuffle();
        playerDeck.DrawCards(5);


        print("Game Reset");
    }
}


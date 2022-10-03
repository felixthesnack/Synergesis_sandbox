using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenTest : MonoBehaviour
{

    public Vector3 endPosition;
    public GameObject Shuffle;
    public Transform startPosition;

    // Start is called before the first frame update
    void Start()
    {
        Shuffle = GameObject.Find("Shuffle");
        endPosition = Shuffle.transform.position;
        startPosition = gameObject.transform;

        startPosition.DOMove(endPosition, 1).SetEase(Ease.InOutBack);
    }

}

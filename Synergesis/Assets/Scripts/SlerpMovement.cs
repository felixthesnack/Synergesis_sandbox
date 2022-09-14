using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpMovement : MonoBehaviour
{
    public GameObject Shuffle;
    private Vector3 endPosition;
    private Vector3 startPosition;
    private Vector3 overshootPosition;
    private float duration = 2f;
    private float elapsedTime;

    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float overshootPercentage;

    // Start is called before the first frame update
    void Start()
    {
        Shuffle = GameObject.Find("Shuffle");
        endPosition = Shuffle.transform.position;
        startPosition = transform.position;
        overshootPosition = endPosition + (endPosition - startPosition) * overshootPercentage;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / duration;
        transform.position = Vector3.LerpUnclamped(startPosition, overshootPosition, curve.Evaluate(percentageComplete));

        float deltaP = 0.001f;
        if ((transform.position - overshootPosition).magnitude < deltaP)
        {
            //start tweening to destinationPos rather than overShootPos. Possibly just
            overshootPosition = endPosition;
        }
    }

    private void FixedUpdate()
    {
        //float deltaP = 0.001f;
        //if ((transform.position - overshootPosition).magnitude < deltaP)
        //{
        //    //start tweening to destinationPos rather than overShootPos. Possibly just
        //    overshootPosition = endPosition;
        //}
    }
}

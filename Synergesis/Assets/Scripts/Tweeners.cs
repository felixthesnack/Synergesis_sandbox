using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tweeners : MonoBehaviour
{
    public Transform start;
    public int maxWidth = 720;

    public void DrawCard(Transform start, Vector3 end, float travelTime)
    {
        start.DOMove(end, travelTime).SetEase(Ease.InOutBack);
    }

}

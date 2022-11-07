using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZoomInvoke : MonoBehaviour
{

    public Button button;

    void OnEnable()
    {
        button = null;
        StartCoroutine(GetButton());
    }

    public void Invoke()
    {
        if (button != null && button.onClick != null)
            button.onClick.Invoke();
    }

    private IEnumerator GetButton()
    {
        yield return new WaitForFixedUpdate();
        if (button == null)
            button = transform.GetChild(0).GetComponent<Button>();
    }
}

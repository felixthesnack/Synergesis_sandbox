using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public Gradient Gradient;

    public Image ChildImage;
    public PlayerDeck PlayerDeck;
    [SerializeField] GameObject Slot;
    [SerializeField] GameObject SynergesisLine;
    public int synergyLevel;

    private void Awake()
    {
        synergyLevel = PlayerDeck.synergyLevel;
    }


    public void LoadSynergyBar()
    {
        int deckCount = PlayerDeck.deckSize;
        int slotCount = this.transform.childCount;

        if (slotCount > synergyLevel)
        {
            slotCount--;
        }

        for (int i = 0; i < (deckCount - slotCount); i++)
        {
            GameObject barSlot = Instantiate(Slot, transform.position, transform.rotation, this.transform) as GameObject;
            barSlot.transform.localScale = Vector3.one;

            if (deckCount == synergyLevel)
            {
                GameObject synergesisLevel = Instantiate(SynergesisLine, transform.position, transform.rotation, this.transform) as GameObject;
                synergesisLevel.transform.localScale = Vector3.one;
            }
        }

        ApplyGradient(deckCount);
    }

    private void ApplyGradient(int slots)
    {
        for (int i = 0; i < slots; i++)
        {
            if (i == synergyLevel)
            {
                slots++;
                continue;
            }
            ChildImage = this.gameObject.transform.GetChild(i).GetChild(0).GetComponent<Image>();
            if (i < synergyLevel)
            {
                float timePoint = (float) i / synergyLevel;
                Color color = Gradient.Evaluate(timePoint);
                ChildImage.color = color;
            }
            if (i > synergyLevel)
            {
                ChildImage.color = Color.red;
            }
        }
    }
}

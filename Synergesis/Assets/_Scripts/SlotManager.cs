using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public Gradient Gradient;

    public Image ChildImage;
    //public PlayerDeck PlayerDeck;
    [SerializeField] GameObject Slot;
    [SerializeField] GameObject SynergesisLine;
    public int synergyLevel;

    private void Awake()
    {
        synergyLevel = PlayerDeck.Instance.synergyLevel;
    }


    public void LoadSynergyBar()
    {
        int deckCount = PlayerDeck.Instance.deckSize;
        int slotCount = transform.childCount;

        if (slotCount == synergyLevel)
        {
            slotCount--;
        }

        for (int i = 0; i < (deckCount - slotCount); i++)
        {
            GameObject barSlot = Instantiate(Slot, transform.position, transform.rotation, transform) as GameObject;
            barSlot.transform.localScale = Vector3.one;

            if (deckCount == synergyLevel)
            {
                GameObject synergesisLevel = Instantiate(SynergesisLine, transform.position, transform.rotation, this.transform) as GameObject;
                synergesisLevel.transform.localScale = Vector3.one;
            }
            
            ApplyGradient(i);
        }

    }

    public void RemoveSlot()
    {
        int deckCount = PlayerDeck.Instance.deckSize;
        int slotCount = transform.childCount;

        GameObject slot = gameObject.transform.GetChild(transform.childCount - 1).gameObject;
        slot.SetActive(false);
        Destroy(slot);

        if (deckCount == synergyLevel)
        {
            slot = gameObject.transform.GetChild(transform.childCount - 1).gameObject;
            slot.SetActive(false);
            Destroy(slot);
        }

        for(int i = 0; i < slotCount; i++)
        {
            ApplyGradient(i);
        }
    }

    private void ApplyGradient(int slot)
    {
            if (slot == synergyLevel)
            {
                slot++;
            }

            ChildImage = gameObject.transform.GetChild(slot).GetChild(0).GetComponent<Image>();

            if (slot < synergyLevel)
            {
                float timePoint = (float) slot / synergyLevel;
                Color color = Gradient.Evaluate(timePoint);
                ChildImage.color = color;
            }
            if (slot > synergyLevel)
            {
                ChildImage.color = Color.red;
            }
    }
}

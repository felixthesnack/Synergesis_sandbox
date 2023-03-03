using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public Gradient Gradient;

    //public Image ChildImage;
    //public PlayerDeck PlayerDeck;
    [SerializeField] GameObject Slot;
    [SerializeField] GameObject SynergesisSlot;
    private int slotCount;
    private int deckCount;
    //public int synergyLevel;

    //private void Awake()
    //{
    //    synergyLevel = PlayerDeck.Instance.synergyLevel;
    //}


    public void LoadSynergyBar()
    {
        deckCount = PlayerDeck.Instance.deckSize;
        slotCount = transform.childCount;

        //if (slotCount == synergyLevel)
        //{
        //    slotCount--;
        //}

        for (int i = 0; i < (deckCount - slotCount); i++)
        {
            //if (slotCount == synergyLevel - 1)
            //{
            //    GameObject synergesisLevel = Instantiate(SynergesisSlot, transform.position, transform.rotation, this.transform) as GameObject;
            //    synergesisLevel.transform.localScale = Vector3.one;
            //} 
            //else
            //{
                GameObject barSlot = Instantiate(Slot, transform.position, transform.rotation, transform) as GameObject;
                barSlot.transform.localScale = Vector3.one;
            //}
        }

        slotCount = transform.childCount;

        for (int i = 0; i < slotCount; i++)
        {
            ApplyGradient(i);
        }

    }

    public void RemoveSlot()
    {
        //int deckCount = PlayerDeck.Instance.deckSize;
        slotCount = transform.childCount;

        GameObject slot = gameObject.transform.GetChild(transform.childCount - 1).gameObject;
        slot.SetActive(false);
        Destroy(slot);

        //if (deckCount == synergyLevel)
        //{
        //    slot = gameObject.transform.GetChild(transform.childCount - 1).gameObject;
        //    slot.SetActive(false);
        //    Destroy(slot);
        //}

        for(int i = 0; i < slotCount; i++)
        {
            ApplyGradient(i);
        }
    }

    private void ApplyGradient(int slotIndex)
    {
            //if (slot == synergyLevel)
            //{
            //    slot++;
            //}

            Image ChildImage = gameObject.transform.GetChild(slotIndex).GetChild(0).GetComponent<Image>();

            //if (slot <= synergyLevel - 1)
            //{
                float timePoint = (float) slotIndex / slotCount;
                Color color = Gradient.Evaluate(timePoint);
                ChildImage.color = color;
            //}
            //if (slot > synergyLevel - 1)
            //{
            //    ChildImage.color = Color.red;
            //}
    }
}

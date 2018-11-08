using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDropHandler : MonoBehaviour, IDropHandler {


    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop "+ eventData.selectedObject.name);

        RectTransform cardSlot = transform as RectTransform;
        if (cardSlot.tag == "CardSlot")
        {
            eventData.selectedObject.transform.SetParent(transform.parent);
            eventData.selectedObject.GetComponent<CardDragHandler>().setStartPos(transform.position);
        }
        else
        {
            eventData.selectedObject.GetComponent<CardDragHandler>().ResetParent();
        }

    }

}

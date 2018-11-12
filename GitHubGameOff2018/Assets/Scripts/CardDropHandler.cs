using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDropHandler : MonoBehaviour, IDropHandler {


    public void OnDrop(PointerEventData eventData)
    {
  
        Draggable dragHandler = eventData.pointerDrag.GetComponent<Draggable>();
        if (dragHandler != null)
        {
            dragHandler.parentToReturnTo = transform;
        }


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDropHandler : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }

        Draggable dragHandler = eventData.pointerDrag.GetComponent<Draggable>();
        if (dragHandler != null)
        {
            dragHandler.placeHolderParent = transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }

        Draggable dragHandler = eventData.pointerDrag.GetComponent<Draggable>();
        if (dragHandler != null && dragHandler.placeHolderParent == this.transform)
        {
            dragHandler.placeHolderParent = dragHandler.parentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
  
        Draggable dragHandler = eventData.pointerDrag.GetComponent<Draggable>();
        if (dragHandler != null)
        {
            dragHandler.parentToReturnTo = transform;
        }


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    //ToDo : need much work... 
    //Ref Code https://forum.unity.com/threads/inventory-drag-and-drop-code.518046/

    private Vector3 startPOS;
    private void Start()
    {
        startPOS = transform.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //transform.localPosition = Vector3.zero;
        transform.position = startPOS;
    }
}

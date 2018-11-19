using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //ToDo : need much work... 
    //Ref Code https://forum.unity.com/threads/inventory-drag-and-drop-code.518046/

    private Vector3 startPOS;
    private GameObject parentObject;
    
    private void Start()
    {
        startPOS = transform.position;
        parentObject = this.transform.parent.gameObject;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        startPOS = transform.position;
        parentObject = this.transform.parent.gameObject;

        transform.SetParent(GameObject.Find("MovingCard").transform);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        transform.position = startPOS;

        if (gameObject.transform.parent.name == "MovingCard")
        {
            //NEW CODE -- NEED TO RESET TO THE LAST PARENT WE HAD
            this.transform.SetParent(parentObject.transform);
        }



        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void setStartPos(Vector3 newStartPos)
    {
        startPOS = newStartPos;
    }

    public void ResetParent()
    {
        this.transform.SetParent(parentObject.transform);
    }
}

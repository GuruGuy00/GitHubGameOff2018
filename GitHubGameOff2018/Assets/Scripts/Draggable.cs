using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public Transform parentToReturnTo = null;
    public Transform placeHolderParent = null;

    public GameObject placeHolder = null;

    private GameObject[] dropZones;

    void Start()
    {
        //Cache a list of drop zones for future use
        dropZones = GameObject.FindGameObjectsWithTag("Dropzone");
        //If we didn't set a Parent To Return To, set it to the Hand gameobject
        if (parentToReturnTo == null)
        {
            GameObject hand = GameObject.FindGameObjectWithTag("Hand");
            if (hand != null)
            {
                parentToReturnTo = hand.transform;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        placeHolder = new GameObject();
        placeHolder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeHolder.AddComponent<LayoutElement>();
        //le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        //le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        //le.flexibleHeight = 0;
        //le.flexibleWidth = 0;

        placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        parentToReturnTo = this.transform.parent;
        placeHolderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;

        HighlightDropZones();
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;

        if (placeHolder.transform.parent != placeHolderParent)
        {
            placeHolder.transform.SetParent(placeHolderParent);
        }

        int newSiblingIndex = placeHolderParent.childCount;
        bool isVertical = placeHolderParent.name == "Played";

        for (int i = 0; i < placeHolderParent.childCount; i++)
        {
            if (isVertical)
            {
                if (this.transform.position.y > placeHolderParent.GetChild(i).position.y)
                {
                    newSiblingIndex = UpdateSiblingIndex(newSiblingIndex, i);
                    break;
                }
            }
            else
            {
                if (this.transform.position.x < placeHolderParent.GetChild(i).position.x)
                {
                    newSiblingIndex = UpdateSiblingIndex(newSiblingIndex, i);
                    break;
                }
            }
        }

        placeHolder.transform.SetSiblingIndex(newSiblingIndex);

    }

    private int UpdateSiblingIndex(int indexToUpdate, int updateIndex)
    {
        indexToUpdate = updateIndex;
        if (placeHolder.transform.GetSiblingIndex() < indexToUpdate)
        {
            indexToUpdate--;
        }
        return indexToUpdate;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Destroy(placeHolder);

        UnhighlightDropZones();
    }

    private void HighlightDropZones()
    {
        foreach (GameObject dropZone in dropZones)
        {
            dropZone.GetComponent<Outline>().enabled = true;
        }
    }

    private void UnhighlightDropZones()
    {
        foreach (GameObject dropZone in dropZones)
        {
            dropZone.GetComponent<Outline>().enabled = false;
        }
    }
}

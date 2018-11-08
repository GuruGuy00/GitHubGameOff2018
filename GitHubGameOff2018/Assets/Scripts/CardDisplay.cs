using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour {

    public Card card;

    //public Text nameTextOld;
    //public Text descriptionTextOld;
    //public Text actionTextOld;

    public TextMeshProUGUI actionText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public Image artworkImage;

    private Move moveManager;

    void Start()
    {
        moveManager = GameObject.FindObjectOfType<Move>();

        if (card != null)
        {
            nameText.text = card.cardName;
            descriptionText.text = card.description;
            actionText.text = card.actionCost.ToString();
            artworkImage.sprite = card.artwork;
        }
    }

    public void PlayCard()
    {
        //moveManager.AddMove(card.moveName);
        //moveManager.SubmitMoves();
    }

}
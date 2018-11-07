﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

    public Card card;

    public Text nameText;
    public Text descriptionText;
    public Text actionText;

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
        moveManager.AddMove(card.moveName);
        moveManager.SubmitMoves();
    }

}
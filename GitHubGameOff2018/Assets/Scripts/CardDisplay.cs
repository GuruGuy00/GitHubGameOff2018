using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour {

    public Card card;

    public TextMeshProUGUI actionText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public Image artworkImage;

    private Move moveManager;

    void Start()
    {
        if (card != null)
        {
            nameText.text = card.cardName;
            descriptionText.text = card.description;
            actionText.text = card.actionCost.ToString();
            artworkImage.sprite = card.artwork;
        }
    }

}
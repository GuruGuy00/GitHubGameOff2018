﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject {

    public string cardName;
    [TextArea]
    public string description;
    public Sprite artwork;
    public int actionCost;
    public string moveName;
    public Vector3Int moveTo;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHearts : MonoBehaviour
{
    private int heartCount = 3;
    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        //The empty hearts are at index 3, 4, and 5
        GameObject emptyHeart1 = transform.GetChild(3).gameObject;
        GameObject emptyHeart2 = transform.GetChild(4).gameObject;
        GameObject emptyHeart3 = transform.GetChild(5).gameObject;
        //The empty hearts are rendered in front of the full hearts.
        //So simply enabling them when HP is below the threshold will display them properly.
        emptyHeart1.SetActive(player.HP < 25);
        emptyHeart2.SetActive(player.HP < 50);
        emptyHeart3.SetActive(player.HP < 75);
    } 
}

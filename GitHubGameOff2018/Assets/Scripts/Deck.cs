using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    public int cardsInHand = 4;
    public GameObject handPanel;

    public List<GameObject> cards;
    public List<GameObject> playdeck;
    public List<GameObject> hand;

	// Use this for initialization
	void Start () {
            playdeck = ShuffleDeck(cards);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private List<T> ShuffleDeck<T>(List<T> cards)
    {
        List<T> randomList = new List<T>();

        System.Random r = new System.Random();
        int randomIndex = 0;
        while (cards.Count > 0)
        {
            randomIndex = r.Next(0, cards.Count);
            randomList.Add(cards[randomIndex]);
            cards.RemoveAt(randomIndex);
        }
        return randomList;
    }

    public void Deal()
    {

        while (hand.Count < 4)
        {
            Instantiate(playdeck[0], handPanel.transform);
            hand.Add(playdeck[0]);
            playdeck.RemoveAt(0);

        }
    }

}

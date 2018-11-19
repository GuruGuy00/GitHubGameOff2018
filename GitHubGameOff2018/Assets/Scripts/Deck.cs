using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    public int cardsInHand = 4;
    public GameObject handPanel;

    public List<GameObject> cards;
    public List<GameObject> playDeck;
    public List<GameObject> hand;
    public List<GameObject> toPlay;

	// Use this for initialization
	void Start () {
            playDeck = ShuffleDeck(cards);
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
            GameObject newGO = Instantiate(playDeck[0], handPanel.transform);
            newGO.name = playDeck[0].name;
            hand.Add(playDeck[0]);
            playDeck.RemoveAt(0);

        }
    }

    public void HandToPlay(GameObject card)
    {
        int index = hand.FindIndex(x => x.name.Equals(card.name));
        toPlay.Add(hand[index]);
        hand.RemoveAt(index);
    }

}

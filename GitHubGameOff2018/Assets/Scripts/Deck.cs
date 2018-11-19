using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    public int cardsInHand = 4;
    public GameObject handPanel;
    public GameObject discardPanel;

    public List<GameObject> cards;
    public List<GameObject> sourceDeck;
    public List<GameObject> playDeck;
    public List<GameObject> hand;
    public List<GameObject> toPlay;
    public List<GameObject> discard;

	// Use this for initialization
	void Start () {
            //playDeck = ShuffleDeck(cards);
            playDeck = ShuffleDeck2();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private List<GameObject> ShuffleDeck2()
    {
        List<GameObject> randomList = new List<GameObject>();
        System.Random r = new System.Random();
        int randomIndex = 0;
        while (cards.Count > 0)
        {
            randomIndex = r.Next(0, cards.Count);
            GameObject newGO = Instantiate(cards[randomIndex],this.transform);
            randomList.Add(newGO);
            cards.RemoveAt(randomIndex);
        }
        return randomList;
    }

    private List<T> ShuffleDeck<T>(List<T> cards)
    {
        List<T> randomList = new List<T>();

        System.Random r = new System.Random();
        int randomIndex = 0;
        while (cards.Count > 0)
        {
            randomIndex = r.Next(0, cards.Count);
            //GameObject newGO = Instantiate(cards[randomIndex], handPanel.transform);
            randomList.Add(cards[randomIndex]);
            cards.RemoveAt(randomIndex);
        }
        return randomList;
    }

    public void Deal()
    {

        while (hand.Count < 4)
        {

            //GameObject newGO = Instantiate(playDeck[0], handPanel.transform);
            //hand.Add(newGO);

            playDeck[0].transform.SetParent(handPanel.transform);

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

    public void PlayToHand(GameObject card)
    {
        int index = toPlay.FindIndex(x => x.name.Equals(card.name));
        hand.Add(hand[index]);
        toPlay.RemoveAt(index);
    }

    public void DiscardCards()
    {
        //Add some fancy stuff to viusaly move the cards to discard pile
        while (toPlay.Count > 0)
        {
            toPlay[0].transform.SetParent(discardPanel.transform);
            discard.Add(toPlay[0]);
            toPlay.RemoveAt(0);
            //Move to visual discard pile

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    public int cardsInHand = 4;
    public bool shuffle = true;
    public GameObject handPanel;
    public GameObject discardPanel;
    public GameObject discardPanel2;

    public Transform playCardsParent;
    public Transform handParent;

    public List<GameObject> cards;
    public List<GameObject> sourceDeck;
    public List<GameObject> playDeck;
    public List<GameObject> discard;
    public List<GameObject> discard2;

    // Use this for initialization
    void Start () {

        if (shuffle)
        {
            playDeck = ShuffleDeck2();
        }
        else
        {
            playDeck = UnshuffledDeck();
        }
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

    private List<GameObject> UnshuffledDeck()
    {
        
        List<GameObject> randomList = new List<GameObject>();

        while (cards.Count > 0)
        {
            GameObject newGO = Instantiate(cards[0], this.transform);
            randomList.Add(newGO);
            cards.RemoveAt(0);
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
        CardDisplay[] hand = GetHand();
        int numCardsToAdd = 4 - hand.Length;
        bool needReshuffle = false;

        if (numCardsToAdd > playDeck.Count)
        {
            needReshuffle = true;
        }

        while (numCardsToAdd > 0 && playDeck.Count > 0)
        {
            GameObject newGO = Instantiate(playDeck[0], handPanel.transform);
            newGO.transform.SetParent(handPanel.transform);
            playDeck.RemoveAt(0);
            numCardsToAdd--;
        }

        if (needReshuffle)
        {
            Debug.Log("RESHUFFLE");
            Reshuffle();
            //Deal differance need
        }
    }

    public void DiscardCards()
    {
        CardDisplay[] cardsToPlay = GetPlayCards();
        //Add some fancy stuff to viusaly move the cards to discard pile
        for (int i = 0; i < cardsToPlay.Length; i++)
        {
            cardsToPlay[i].transform.SetParent(discardPanel.transform);
            discard.Add(cardsToPlay[i].gameObject);
            //Move to visual discard pile
        }

        //Added this so the cards stack off screen, to fix the over stack in the discards pile
        while (discard.Count > 3)
        {
            discard[0].transform.SetParent(discardPanel2.transform);
            discard2.Add(discard[0]);
            discard.RemoveAt(0);
        }

    }

    private CardDisplay[] GetHand()
    {
        return handParent.GetComponentsInChildren<CardDisplay>();
    }

    private CardDisplay[] GetPlayCards()
    {
        return playCardsParent.GetComponentsInChildren<CardDisplay>();
    }

    private void Reshuffle()
    {
        playDeck.AddRange(discard);
        playDeck.AddRange(discard2);

        discard.Clear();
        discard2.Clear();

        //if (shuffle)
        //{
        //    playDeck = ShuffleDeck2();
        //}
        //else
        //{
        //    playDeck = UnshuffledDeck();
        //}
    }
}

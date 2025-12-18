using UnityEngine;
using System.Collections.Generic;

public class Deck
{
    List<Card> cards = new();

    public Deck()
    {
        cards = Card.GetDeck();
    }

    public Deck(Deck copy)
    {
        cards = new List<Card>(copy.cards);
    }

    public Card GetTopCard()
    {
        Debug.Log($"There are {cards.Count} cards in the deck");
        Card card = cards[0];
        cards.Remove(card);
        return card;
    }

    public void ReturnCard(Card card)
    {
        cards.Add(card);
    }

    public Card Random()
    {
        Card card = cards[UnityEngine.Random.Range(0, cards.Count)];
        cards.Remove(card);
        return card;
    }

    public bool IsEmpty()
    {
        return cards.Count == 0;
    }

    public void Shuffle()
    {
        Deck unshuffled = new Deck(this);
        cards.Clear();
        while (!unshuffled.IsEmpty())
        {
            Card card = unshuffled.Random();
            cards.Add(card);
        }
    }
}

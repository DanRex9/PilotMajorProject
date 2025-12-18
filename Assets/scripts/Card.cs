using UnityEngine;
using System.Collections.Generic;

public class Card
{
    public enum Value
    {
        Ace = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    public enum Suit
    {
        Spades = 1,
        Clubs,
        Hearts,
        Diamonds
    }

    Value value;
    Suit suit;

    public void PrintCard()
    {
        Debug.Log($"Value {value} Suit {suit}");
    }

    public Value GetCardValue()
    {
        return value;
    }

    public Suit GetSuit()
    {
        return suit;
    }

    public bool IsOdd()
    {
        return (int)value % 2 == 1;
    }

    public bool IsEven()
    {
        return (int)value % 2 == 0; 
    }

    public static bool IsHigher(Card a, Card b)
    {
        return (int)a.value >= (int)b.value;
    }

    public static bool IsLower(Card a, Card b)
    {
        return (int)a.value <= (int)b.value;
    }

    public bool Inside(Card a, Card b)
    {
        int minimum = Mathf.Min((int)a.value, (int)b.value);
        int maximum = Mathf.Max((int)a.value, (int)b.value);
        return (int)value <= maximum && (int)value >= minimum;
    }

    public bool Outside(Card a, Card b)
    {
        return !Inside(a, b);
    }

    public bool IsSuit(Suit suit)
    {
        return this.suit == suit;
    }

    public static List<Card> GetDeck()
    {
        List<Card> deck = new();
        for (int suit = (int)Suit.Spades; suit <= (int)Suit.Diamonds; suit++)
        {
            for (int value = (int)Value.Ace; value <= (int)Value.King; value++)
            {
                Card card = new ();
                card.value = (Value)value;
                card.suit = (Suit)suit;
                deck.Add(card);
            }
        }
        return deck;
    }
}

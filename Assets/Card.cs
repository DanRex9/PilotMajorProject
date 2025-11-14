using UnityEngine;

public class Card
{
    enum Value
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

    public static Card Random()
    {
        Card card = new Card();
        card.value = (Value)UnityEngine.Random.Range((int)Value.Ace, (int)Value.King + 1);
        card.suit = (Suit)UnityEngine.Random.Range((int)Suit.Spades, (int)Suit.Diamonds + 1);
        Debug.Log($"Value {card.value} Suit {card.suit}");
        return card;
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
}

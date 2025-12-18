using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Dealer : MonoBehaviour
{
    [SerializeField] TMP_Text message;
    [SerializeField] TMP_Text answer1;
    [SerializeField] TMP_Text answer2;
    [SerializeField] TMP_Text answer3;
    [SerializeField] TMP_Text answer4;
    [SerializeField] SpriteRenderer theCard;
    [SerializeField] Sprite cardBack;
    int answerPressed = 0;
    bool win = false;
    List<Card> drawn = new ();
    Deck deck = new ();

    public void AnswerPressed(int number)
    {
        answerPressed = number;
    }

    int round = 1;
    enum Phase
    {
        Question,
        Answer,
        Deal,
        WinLose
    }
    Phase phase = Phase.Question;

    void Start()
    {
        round = 1;
        phase = Phase.Question;
        message.gameObject.SetActive(true);
        deck.Shuffle();
    }

    void ShowButton(TMP_Text button, string text)
    {
        button.text = text;
        button.transform.parent.gameObject.SetActive(true);
    }

    void HideButton(TMP_Text button)
    {
        button.transform.parent.gameObject.SetActive(false);   
    }

    void DisplayQuestionForRound()
    {
        switch (round)
        {
            case 1:
                message.text = "Odd or Even";
                ShowButton(answer1, "Odd");
                ShowButton(answer2, "Even");
                HideButton(answer3);
                HideButton(answer4);
                break;
            case 2:
                message.text = "Higher or Lower";
                ShowButton(answer1, "Higher");
                ShowButton(answer2, "Lower");
                HideButton(answer3);
                HideButton(answer4);
                break;
            case 3:
                message.text = "Inside or Outside";
                ShowButton(answer1, "Inside");
                ShowButton(answer2, "Outside");
                HideButton(answer3);
                HideButton(answer4);
                break; 
            case 4:
                message.text = "Guess the Suit";
                ShowButton(answer1, "Spades");
                ShowButton(answer2, "Clubs");
                ShowButton(answer3, "Hearts");
                ShowButton(answer4, "Diamonds");
                break;
        }
    }

    Card DealCard()
    {
        Card card = deck.GetTopCard();
        card.PrintCard();
        string dir = card.GetSuit().ToString().ToUpper();
        string value = card.GetCardValue().ToString();
        string suit = card.GetSuit().ToString();
        string path = $"{dir}/{value}_Of_{suit}";
        Debug.Log(path);
        var sprite = Resources.Load<Sprite>(path);
        theCard.sprite = sprite;
        return card;
    }

    void DealForRound()
    {
        Card card = DealCard(); 
        switch(round)
        {
            case 1:
                if (answerPressed == 1)
                {
                    win = card.IsOdd();
                }
                else if (answerPressed == 2)
                {
                    win = card.IsEven();
                }
                break;

            case 2:
                if (answerPressed == 1)
                {
                    win = Card.IsHigher(card, drawn[0]);
                }
                if (answerPressed == 2)
                {
                    win = Card.IsLower(card, drawn[0]);
                }
                break;

            case 3: 
                if (answerPressed == 1)
                {
                    win = card.Inside(drawn[0], drawn[1]);
                }  
                if (answerPressed == 2)
                {
                    win = card.Outside(drawn[0], drawn[1]);
                }  
                break;

            case 4:
                win = card.IsSuit((Card.Suit)answerPressed);
                break;
        }
        drawn.Add(card);
    }

    void Update()
    {
        switch (phase)
        {
            case Phase.Question:
                win = false;
                answerPressed = 0;
                DisplayQuestionForRound();
                phase = Phase.Answer;
                break;

            case Phase.Answer:
                if (answerPressed > 0)
                {
                    phase = Phase.Deal;
                }
                break;

            case Phase.Deal:
                DealForRound();
                phase = Phase.WinLose;
                break;

            case Phase.WinLose:
                Debug.Log(win? "Win" : "Lose");
                if (win)
                {
                    round++;
                }
                else
                {
                    theCard.sprite = cardBack;
                    round = 1;
                    foreach (Card card in drawn)
                    {
                        deck.ReturnCard(card);
                    }
                    drawn.Clear();
                    deck.Shuffle();
                }
                phase = Phase.Question;
                break;

        }
    }
}

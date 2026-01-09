using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class Dealer : MonoBehaviour
{
    [SerializeField] TMP_Text message;
    [SerializeField] TMP_Text answer1;
    [SerializeField] TMP_Text answer2;
    [SerializeField] TMP_Text answer3;
    [SerializeField] TMP_Text answer4;
    [SerializeField] TMP_Text playerCoinsDisplay;
    [SerializeField] TMP_Text potDisplay;
    [SerializeField] TMP_Text dealerCoinsDisplay;
    [SerializeField] SpriteRenderer [] cards;
    [SerializeField] Sprite cardBack;
    int answerPressed = 0;
    bool win = false;
    List<Card> drawn = new ();
    Deck deck = new ();
    int playerCoins;
    int dealerCoins;
    int bet;

    public void AnswerPressed(int number)
    {
        answerPressed = number;
    }

    int round = 0;
    enum Phase
    {
        Question,
        Answer,
        Bet,
        Deal,
        Result,
        Retry
    }
    Phase phase = Phase.Question;

    void Start()
    {
        bet = 1;
        HideCards();
        UpdatePlayerCoins(20);
        UpdateDealerCoins(10);
        UpdatePot(0);
        round = 0;
        phase = Phase.Question;
        message.gameObject.SetActive(true);
        deck.Shuffle();
    }

    void UpdatePlayerCoins(int coins)
    {
        playerCoins += coins;
        playerCoinsDisplay.text = $"Player coins : {playerCoins}";
    }

    void UpdateDealerCoins(int coins)
    {
        dealerCoins += coins;
        dealerCoinsDisplay.text = $"Dealer coins : {dealerCoins}";
    }

    void UpdatePot(int bet)
    {
        if (bet == 0)
        {
            potDisplay.gameObject.SetActive(false);
        }
        else
        {
            potDisplay.text = $"Pot : {bet * 2}";
            potDisplay.gameObject.SetActive(true);
        }
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
            case 0:
                message.text = $"Bet {bet}?";
                ShowButton(answer1, "-");
                ShowButton(answer2, "+");
                ShowButton(answer3, "bet");
                HideButton(answer4);
                break;
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

    void DoYouWantToRetry(string winOrLose)
    {
        answerPressed = 0;
        message.text = $"{winOrLose}: Do you want to retry?";
        ShowButton(answer1, "Yes");
        ShowButton(answer2, "No");
        HideButton(answer3);
        HideButton(answer4);
    }

    Card DealCard()
    {
        //this code makes the game show the card that has been drawn by the dealer
        Card card = deck.GetTopCard();
        card.PrintCard();
        string dir = card.GetSuit().ToString().ToUpper();
        string value = card.GetCardValue().ToString();
        string suit = card.GetSuit().ToString();
        string path = $"{dir}/{value}_Of_{suit}";
        Debug.Log(path);
        var sprite = Resources.Load<Sprite>(path);
        cards[round-1].sprite = sprite;
        return card;
    }

    void DealForRound()
    {
        Card card = DealCard(); 
        switch(round)
        {
            //odd or even 
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
                //higher or lower
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
                //inside or outside
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
                //guess the suit
                win = card.IsSuit((Card.Suit)answerPressed);
                break;
        }
        drawn.Add(card);
    }

    // return 1 if finished betting
    int UpdateBet()
    {
        int nextRound = 0;
        switch (answerPressed)
        {
            case 1:
                if (bet > 1)
                {
                    bet--;
                }
                break;
            case 2:
                if (bet < playerCoins && bet < dealerCoins)
                {
                    bet++;
                }
                break;
            case 3:
                UpdatePlayerCoins(-bet);
                UpdateDealerCoins(-bet);
                UpdatePot(bet);
                nextRound = 1;
                break;
        }
        return nextRound;
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
                    phase = (round == 0)? Phase.Bet : Phase.Deal;
                }
                break;

            case Phase.Bet:
                round = UpdateBet();
                phase = Phase.Question;
                break;

            case Phase.Deal:
                DealForRound();
                phase = Phase.Result;
                break;
            
            case Phase.Result:
                if (win)
                {
                    if (round == 4)
                    {
                        UpdatePlayerCoins(bet * 2);
                        UpdatePot(0);
                        DoYouWantToRetry("You won");
                        phase = Phase.Retry;
                    }
                    else
                    {
                        round++;
                        cards[round - 1].sprite = cardBack;
                        phase = Phase.Question;
                    }
                }
                else
                {
                    UpdateDealerCoins(bet * 2);
                    UpdatePot(0);
                    if (playerCoins == 0)
                    {
                        SceneManager.LoadScene("Main Menu");
                    }
                    DoYouWantToRetry("You lost");
                    phase = Phase.Retry;
                }
                break;

            case Phase.Retry:
                if (answerPressed == 1)
                {
                    bet = 1;
                    HideCards();
                    round = 0;
                    foreach (Card card in drawn)
                    {
                        deck.ReturnCard(card);
                    }
                    drawn.Clear();
                    deck.Shuffle();
                    phase = Phase.Question;
                }
                else if (answerPressed == 2)
                {
                    SceneManager.LoadScene("Main Menu");
                }
                break;

        }
    }

    void HideCards()
    {
        foreach (SpriteRenderer card in cards)
        {
            card.sprite = null;
        }
        cards[0].sprite = cardBack;
    }
}

using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody body;

    void Start ()
    {
        body = GetComponent<Rigidbody>();
    }

    public void Pickup()
    {
        body.useGravity = false;
        body.isKinematic = true;
        Debug.Log($"Coin {this} is over the pot? {Pot.instance.IsOverPot(this)}");
        if (Pot.instance.IsOverPot(this))
        {
            Dealer.instance.RemovePlayerCoin();
        }
    }

    public void Follow(Vector3 position)
    {
        Pot.instance.Highlight(this);
        transform.position = position;
    }

    public void Drop()
    {
        body.useGravity = true;
        body.isKinematic = false;
        Debug.Log($"Coin {this} is over the pot? {Pot.instance.IsOverPot(this)}");
        if (Pot.instance.IsOverPot(this))
        {
            Dealer.instance.AddPlayerCoin();
        }
        Pot.instance.Highlight(null);
    }
}

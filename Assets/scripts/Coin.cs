using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody body;
    Vector3 target;
    Pile pot;

    void Start ()
    {
        body = GetComponent<Rigidbody>();
        pot = Dealer.instance.potPile;
    }

    public void Pickup()
    {
        body.useGravity = false;
        body.isKinematic = true;
        //Debug.Log($"Coin {this} is over the pot? {pot.IsOverPile(this)}");
        if (pot.IsOverPile(this))
        {
            Dealer.instance.RemovePlayerCoin();
        }
        else
        {
            target = transform.position;
            //Debug.Log($"Target position {target}");
        }
    }

    public void Follow(Vector3 position)
    {
        pot.Highlight(this);
        transform.position = position;
    }

    public void Drop()
    {
        //Debug.Log($"Coin {this} is over the pot? {pot.IsOverPile(this)}");
        if (pot.IsOverPile(this))
        {
            Dealer.instance.AddPlayerCoin();
        }
        else
        {
            transform.position = target;
            //Debug.Log($"Target position {target}");
        }
        body.useGravity = true;
        body.isKinematic = false;
        pot.Highlight(null);
    }

    public void MoveTo(Pile target)
    {
        body.useGravity = false;
        body.isKinematic = true;
        transform.position = target.GetRandomDropPoint();
        body.useGravity = true;
        body.isKinematic = false;
    }
}

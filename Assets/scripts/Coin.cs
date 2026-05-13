using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody body;
    Vector3 target;
    Pile pot;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start ()
    {
        body = GetComponent<Rigidbody>();
        pot = Dealer.instance.potPile;
    }

    public void Pickup()
    {
        //play pickup sound
        audioManager.PlaySound(Sounds.coinpickup);
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
        //play drop sound
        //audioManager.PlaySFX(audioManager.coindrop);
        //Debug.Log($"Coin {this} is over the pot? {pot.IsOverPile(this)}");
        if (pot.IsOverPile(this) && Dealer.instance.CanCoverRaise(1))
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
        audioManager.PlaySound(Sounds.coindrop);
        body.useGravity = false;
        body.isKinematic = true;
        transform.position = target.GetRandomDropPoint();
        body.useGravity = true;
        body.isKinematic = false;
    }
}

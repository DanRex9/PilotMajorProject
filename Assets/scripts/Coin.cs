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
    }

    public void Follow(Vector3 position)
    {
        transform.position = position;
    }

    public void Drop()
    {
        body.useGravity = true;
        body.isKinematic = false;
    }
}

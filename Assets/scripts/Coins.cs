using UnityEngine;
using UnityEngine.InputSystem;

public class Coins : MonoBehaviour
{
    public float distance;
    public LayerMask coinsMask;
    Camera theCamera;
    Coin coin;
    Plane coinPlane;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = theCamera.ScreenPointToRay(Mouse.current.position.value);
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, coinsMask))
            {
                coin = hit.collider.gameObject.GetComponent<Coin>();
                if (coin)
                {
                    coin.Pickup();
                    coinPlane = new Plane(Vector3.up, coin.transform.position + Vector3.up * distance);                
                }
              
            }
        }
        if (coin)
        {
            if (coinPlane.Raycast(ray, out float lengthAlongRay))
            {
                coin.Follow(ray.GetPoint(lengthAlongRay));
            }
        }
        if (coin && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            coin.Drop();
            coin = null;
        }
    }
}

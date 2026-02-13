using UnityEngine;
using UnityEngine.InputSystem;

public class Coins : MonoBehaviour
{
    public float distance;
    Camera theCamera;
    Coin coin;
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
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                coin = hit.collider.gameObject.GetComponent<Coin>();
                if (coin)
                {
                    coin.Pickup();                
                }
            }
        }
        if (coin)
        {
            coin.Follow(ray.GetPoint(distance));
            //Debug.Log($"Drag {Mouse.current.position.x.value}");
        }
        if (coin && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            coin.Drop();
            coin = null;
        }
    }
}

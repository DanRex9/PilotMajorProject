using UnityEngine;
using System.Collections.Generic;

public class Pot : MonoBehaviour
{
    public static Pot instance { get; private set; }
    public Color highlight;
    public Color normal;
    private Material material;
    List<Coin> overPot = new ();

    void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
        instance = this;
    }

    public void Highlight(Coin coin)
    {
        material.color = IsOverPot(coin) ? highlight : normal;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Coin>(out Coin coin))
        {
            Debug.Log($"Coin {coin} is over the pot");
            overPot.Add(coin);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Coin>(out Coin coin))
        {
            Debug.Log($"Coin {coin} is not over the pot");
            overPot.Remove(coin);
        }
    }

    public bool IsOverPot(Coin coin)
    {
        return overPot.Contains(coin);
    }
}

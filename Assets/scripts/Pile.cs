using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Pile : MonoBehaviour
{
    public int layer;
    public Color highlight;
    public Color normal;
    private Material material;
    private Bounds bounds;
    List<Coin> overPile = new ();

    void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer)
        {
            material = renderer.material;        
        }
        Collider collider = GetComponent<Collider> ();
        if (collider)
        {
            bounds = collider.bounds;
        }
    }

    public void Highlight(Coin coin)
    {
        if (material)
        {
            material.color = IsOverPile(coin) ? highlight : normal;        
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Coin>(out Coin coin))
        {
            //Debug.Log($"Coin {coin} is over the {name}");
            overPile.Add(coin);
            coin.gameObject.layer = layer;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Coin>(out Coin coin))
        {
            //Debug.Log($"Coin {coin} is not over the {name}");
            overPile.Remove(coin);
        }
    }

    public bool IsOverPile(Coin coin)
    {
        return overPile.Contains(coin);
    }

    public void SendCoins(int coins, Pile target)
    {
        StartCoroutine(SendCoinsRoutine(coins, target));
    }

    IEnumerator SendCoinsRoutine(int coins, Pile target)
    {
        for (int i = 0; i < coins; i++)
        {
            int last = overPile.Count - 1;
            Coin coin = overPile[last];
            coin.MoveTo(target);
            yield return new WaitUntil(() => overPile.Count == last);
        }
    }

    public Vector3 GetRandomDropPoint()
    {
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;
        return new Vector3(Random.Range(min.x, max.x), max.y, Random.Range(min.z, max.z));
    }
}

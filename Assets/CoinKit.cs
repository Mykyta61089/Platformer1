using UnityEngine;

public class CoinKit : MonoBehaviour
{
    public float points = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        collision.GetComponent<Coin_Component>().AddPoints(points);
        Destroy(gameObject);
    }
    }
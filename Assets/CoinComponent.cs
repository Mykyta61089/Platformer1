using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Coin_Component : MonoBehaviour
{
    private float points;

    public delegate void CoinEventHandler(float currentPoints, float amountChanged);
    public event CoinEventHandler CoinAmountChanged;
    private void Start()
    {
        AddPoints(0);
    }
    public void AddPoints(float amount)
    {
        points += amount;
        CoinAmountChanged?.Invoke(points, amount);
    }
}
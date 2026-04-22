
using TMPro;
using UnityEngine;

public class UI_CoinDisplay : MonoBehaviour
{
    public Coin_Component coinComp;
    public TextMeshProUGUI coinText;

    private void awoke()
    {
        coinComp.CoinAmountChanged += Coin_Comp_CoinAmountChanged;
    }

    private void Coin_Comp_CoinAmountChanged(float CurrentPoints, float ammountChanged)
    {
        coinText.text = CurrentPoints.ToString();

    }
}
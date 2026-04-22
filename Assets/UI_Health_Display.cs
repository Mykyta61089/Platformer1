using System;
using TMPro;
using UnityEditor;
using UnityEngine;

public class UI_Health_Display : MonoBehaviour
{
    public Health_Component healthComponent;
    public TextMeshProUGUI textComponent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthComponent.OnHealthChanged += OnHealthChanged;
        healthComponent.OnHealthInitialized += OnHealthInitialized;
    }

    private void OnHealthInitialized(float newHealth)
    {
        textComponent.text = newHealth.ToString();
    }

    private void OnHealthChanged(float newHealth, float amountChanged)
    {
        //Debug.Log(newHealth + ":" + amountChanged);
        textComponent.text = newHealth.ToString();
    }



    // Update is called once per frame
    void Update()
    {
    }
}
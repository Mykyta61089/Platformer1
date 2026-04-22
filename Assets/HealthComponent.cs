using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Health_Component : MonoBehaviour
{
    private float health = 10;
    public float maxHealth = 10;
    private bool invincibility;

    public delegate void OnHealthInitializedHandler(float newHealth);
    public delegate void OnHealthChangeHandler(float newHealth, float amountChanged);
    public event OnHealthChangeHandler OnHealthChanged;
    public event OnHealthInitializedHandler OnHealthInitialized;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnHealthInitialized?.Invoke(health);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddDamage(float damage)
    {
        //Debug.Log(health);
        if (!invincibility)
        {
            health -= damage;
            OnHealthChanged?.Invoke(health, damage);
            invincibility = true;
            StartCoroutine(ResetInvincibility(2));
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }

    IEnumerator ResetInvincibility(float resetTime)
    {
        yield return new WaitForSeconds(resetTime);
        invincibility = false;
    }


    public void AddHealth(float HealingValue)
    {
        health += HealingValue;

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        OnHealthChanged?.Invoke(health, HealingValue);
        Debug.Log(health);
    }
}
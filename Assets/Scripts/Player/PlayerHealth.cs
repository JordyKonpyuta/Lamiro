using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    // Singleton to be accessible everywhere
    private static PlayerHealth _instance;

    public static PlayerHealth Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Player is null!");
            }

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    
    private int _maxHealth = 100;
    private int _currentHealth = 100;

    void Start()
    {
        SetHealth(_maxHealth);
    }

    // Setter for Health
    public void SetHealth(int h)
    {
        _currentHealth = h;
    }

    public void SetMaxHealth(int mh)
    {
        _maxHealth = mh;
    }

    // Getter for Health
    public int GetHealth()
    {
        return _currentHealth;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }
    
    // Take Damage
    public void TakeDamage(int d)
    {
        _currentHealth -= d;
        ProgressBar.Instance.SetBarPercentage(_currentHealth, _maxHealth);
        if (_currentHealth <= 0)
        {
            Death();
        }
    }
    
    // Death Event
    public void Death()
    {
        Destroy(this);
    }
}

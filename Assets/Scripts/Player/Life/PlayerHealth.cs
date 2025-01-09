using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int _maxHealth = 3;
    private int _currentHealth = 3;

    // -------------------- //
    //       FUNCTIONS      //
    // -------------------- //
    
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
        print(_currentHealth);
        //ProgressBar.Instance.SetBarPercentage(_currentHealth, _maxHealth);
        if (_currentHealth <= 0)
        {
            Death();
        }
    }
    
    // Death Event
    public void Death()
    {
        Destroy(gameObject);
    }
}

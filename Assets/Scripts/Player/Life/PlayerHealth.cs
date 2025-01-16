using System;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerHealth : MonoBehaviour
{
    private int _maxHealth = 3;
    private int _currentHealth = 3;

    public AudioResource[] damageSounds;

    public GameObject vfx;

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
        gameObject.GetComponentInParent<AudioSource>().resource = damageSounds[UnityEngine.Random.Range(0, damageSounds.Length - 1)];
        gameObject.GetComponentInParent<AudioSource>().Play();
        vfx.SetActive(true);
        vfx.GetComponent<ParticleSystem>().Play();
        Invoke(nameof(DesactivateVFX), 0.5f);
        if (_currentHealth <= 0)
        {
            Death();
        }
    }
    
    // Death Event
    public void Death()
    {
        Time.timeScale = 0;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        HUD.Instance.SetVisualHealth();
        GameOver.Instance.Animation();
    }

    private void DesactivateVFX()
    {
        vfx.SetActive(false);
        vfx.GetComponent<ParticleSystem>().Stop();
    }
}

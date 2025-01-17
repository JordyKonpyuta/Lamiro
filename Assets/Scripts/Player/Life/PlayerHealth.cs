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

    void Awake()
    {
        
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
        gameObject.GetComponent<AllPlayerReferences>().HUDref.SetVisualHealth();
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
        gameObject.GetComponent<AllPlayerReferences>().HUDref.SetVisualHealth();
        GameOver.Instance.Animation();
    }

    private void DesactivateVFX()
    {
        vfx.SetActive(false);
        vfx.GetComponent<ParticleSystem>().Stop();
    }
}

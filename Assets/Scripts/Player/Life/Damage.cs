using System;
using Unity.VisualScripting;
using UnityEngine;
using Timer = System.Timers.Timer;

public class Damage : MonoBehaviour
{
    private PlayerHealth _playerHealthRef;
    private CapsuleCollider _collisionBody;
    
    public float totalInvulnerableTime = 1.5f;
    private float _currentInvulnerabilityTime = -1f;
    private bool _isInvulnerable = false;
    
    // -------------------- //
    //       FUNCTIONS      //
    // -------------------- //
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerHealthRef = gameObject.transform.parent.GetComponent<PlayerHealth>();
        _collisionBody = gameObject.GetComponent<CapsuleCollider>();
        _currentInvulnerabilityTime = totalInvulnerableTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isInvulnerable)
        {
            _currentInvulnerabilityTime -= Time.deltaTime;
            if (_currentInvulnerabilityTime <= 0)
            {
                _isInvulnerable = false;
                _currentInvulnerabilityTime = totalInvulnerableTime;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Enemy") || other.CompareTag("EnemyBullet")) && !_isInvulnerable)
        {
            _playerHealthRef.TakeDamage(1);
            _isInvulnerable = true;
        }
    }
}

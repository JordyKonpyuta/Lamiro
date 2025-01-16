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
        
    }

    void ResetInvulnerability()
    {
        _isInvulnerable = false;
    }

    void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("Enemy") || other.CompareTag("EnemyBullet")) && !_isInvulnerable)
        {
            _isInvulnerable = true;
            Invoke(nameof(ResetInvulnerability), 1.5f);
            _playerHealthRef.TakeDamage(other.GetComponent<Ennemy>().GetAttack());
            _playerHealthRef.gameObject.GetComponent<AllPlayerReferences>().HUDref.SetVisualHealth();
        }
    }
}

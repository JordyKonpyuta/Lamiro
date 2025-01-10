using System;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // All Colliders
    private CapsuleCollider _playerBodyRef;
    private BoxCollider _damageArea;
    
    // Enemy Refs
    private List<Ennemy> _allEnemies = new();
    
    // -------------------- //
    //       FUNCTIONS      //
    // -------------------- //
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerBodyRef = transform.parent.GetComponent<CapsuleCollider>();
        _damageArea = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Sword
        if (Input.GetButtonDown("SwordAttack"))
        {
            SwordAttack();
        }
    }

    private void SwordAttack()
    {
        if (_allEnemies == null) return;
        if (_allEnemies.Count <= 0) return;
        foreach (var curEnemy in _allEnemies)
        {
            curEnemy.GetComponent<Ennemy>().TakeDamage(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _allEnemies.Add(other.gameObject.GetComponent<Ennemy>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _allEnemies.Remove(other.gameObject.GetComponent<Ennemy>());
        }
    }
}

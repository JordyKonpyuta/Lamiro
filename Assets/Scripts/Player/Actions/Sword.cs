using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class Sword : MonoBehaviour
{
    // All Colliders
    private CapsuleCollider _playerBodyRef;
    private BoxCollider _damageArea;

    // Enemy Refs
    private List<Ennemy> _allEnemies = new();
    private List<Interactable> _allInteractables = new();
    private List<Interactable> _allRemovedInteractables = new();
    
    // All Booleans
    private bool _canAttack = true;
    
    // Audio
    public AudioResource[] swordSounds;
    private AudioSource _audioSource;
    
    // VFX
    public GameObject vfx;

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
        if (Input.GetButtonDown("SwordAttack") && _canAttack)
        {
            SwordAttack();
            _canAttack = false;
            Invoke(nameof(ResetAttack), 0.5f);
        }
    }

    private void Awake()
    {
        _audioSource = gameObject.GetComponentInParent<AudioSource>();
    }

    private void SwordAttack()
    {
        if (_allEnemies == null && _allInteractables == null)
        {
            PlaySound(0);
            return;
        }
        
        vfx.SetActive(true);
        vfx.GetComponent<ParticleSystem>().Play();

        if (_allEnemies.Count <= 0 && _allInteractables.Count <= 0)
        {
            PlaySound(0);
            return;
        }
        
        PlaySound(1);
        
        foreach (var curEnemy in _allEnemies.ToList())
        {
            if (!curEnemy.isActiveAndEnabled) _allEnemies.Remove(curEnemy);
            else
                curEnemy.GetComponent<Ennemy>().TakeDamage(1);
        }
        foreach (var curInteractable in _allInteractables.ToList())
        {
            if (curInteractable) _allInteractables.Remove(curInteractable);
            if (!curInteractable.isActiveAndEnabled) _allInteractables.Remove(curInteractable);
            else
            {
                curInteractable.OnInteract();
                if (curInteractable.CompareTag("Grass"))
                {
                    _allRemovedInteractables.Add(curInteractable);
                    PlaySound(UnityEngine.Random.Range(2, 4));
                }
                
            }
        }
        if (_allRemovedInteractables.Count > 0)
            Invoke(nameof(RemoveFromInteractable), 0.05f);
    }

    private void RemoveFromInteractable()
    {
        print("test");
        foreach (var curInteractable in _allRemovedInteractables)
        {
            _allInteractables.Remove(curInteractable);
            print(curInteractable.name);
        }
    }

    private void ResetAttack()
    {
        
        vfx.GetComponent<ParticleSystem>().Stop();
        vfx.SetActive(false);
        _canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _allEnemies.Add(other.gameObject.GetComponent<Ennemy>());
        }

        if (other.gameObject.CompareTag("Interactable") || other.gameObject.CompareTag("Grass"))
        {
            _allInteractables.Add(other.gameObject.GetComponent<Interactable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _allEnemies.Remove(other.gameObject.GetComponent<Ennemy>());
        }

        if (other.gameObject.CompareTag("Interactable") || other.gameObject.CompareTag("Grass"))
        {
            _allInteractables.Remove(other.gameObject.GetComponent<Interactable>());
        }
    }

    private void PlaySound(int index)
    {
        _audioSource.resource = swordSounds[index];
        _audioSource.Play();
    }
}

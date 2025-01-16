using System;
using UnityEngine;
using UnityEngine.Audio;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    private CapsuleCollider _playerBodyRef;

    private bool _canAttack = true;

    public GameObject vfx;

    public AudioResource[] bulletSounds;
    private AudioSource _audioSource;
    
    // -------------------- //
    //       FUNCTIONS      //
    // -------------------- //
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerBodyRef = transform.GetComponent<CapsuleCollider>();
    }

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gun
        if (Input.GetButtonDown("GunAttack") && _canAttack)
        {
            GunAttack();
            _canAttack = false;
            Invoke(nameof(ResetAttack), 5f);
        }
    }

    private void ResetAttack()
    {
        _canAttack = true;
    }    
    
    private void GunAttack()
    {
        Instantiate(bulletPrefab, transform.position + transform.forward * 0.25f, transform.rotation);
        PlaySound();
    }

    private void PlaySound()
    {
        _audioSource.resource = bulletSounds[UnityEngine.Random.Range(0, bulletSounds.Length - 1)];
        vfx.SetActive(true);
        vfx.GetComponent<ParticleSystem>().Play();
        _audioSource.Play();
    }
}

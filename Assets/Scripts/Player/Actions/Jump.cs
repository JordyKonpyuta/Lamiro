using System;
using UnityEngine;
using UnityEngine.Audio;

public class Jump : MonoBehaviour
{
    public float jumpForce = 10.0f;
    
    private bool _bIsGrounded = true;
    private LayerMask _groundLayer;

    private AudioSource _audioSource;
    public AudioResource jumpSound;

    private string[] _allLayerNames;

    private Rigidbody _playerBodyRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _allLayerNames = new[] { "Ground", "Default", "Flower" };
        _groundLayer = LayerMask.NameToLayer("Ground");
        _groundLayer = LayerMask.GetMask("Ground");

    _playerBodyRef = transform.GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Detect Ground
        RaycastHit hitCast;
        _bIsGrounded = Physics.Raycast(transform.position, Vector3.down, out hitCast, 1.5f, LayerMask.GetMask(_allLayerNames));
        
        // Jump
        if (_bIsGrounded && Input.GetButtonDown("Jump"))
        {
            _playerBodyRef.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            PlaySound();
        }
    }

    private void PlaySound()
    {
        _audioSource.resource = jumpSound;
        _audioSource.Play();
    }
}

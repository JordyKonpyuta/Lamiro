using System;
using UnityEngine;
using UnityEngine.Audio;

public class Spaceship : MonoBehaviour
{
    public AudioResource healSound;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioSource.resource = healSound;
    }

    private void Start()
    {
        SpaceshipPopUp.Instance.GetComponent<Canvas>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Inventory.Instance.spaceshipPieces < 5)
            {
                SpaceshipPopUp.Instance.PopUp();
                other.GetComponent<PlayerHealth>().SetHealth(other.GetComponent<PlayerHealth>().GetMaxHealth());
                HUD.Instance.SetVisualHealth();
                _audioSource.Play();
            }

            else
            {
                print("Congratulations !");
                Inventory.Instance.CancelInvoke(nameof(Inventory.Instance.SetTimer));
                EndGame.Instance.OnPopUp();
            }   
        }
    }

    private void DisablePopUp()
    {
        SpaceshipPopUp.Instance.GetComponent<Animator>().SetBool("IsOpen", false);
        SpaceshipPopUp.Instance.GetComponent<Canvas>().enabled = false;
    }
    
}

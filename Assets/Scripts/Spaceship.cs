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
            if (other.gameObject.GetComponent<AllPlayerReferences>().invRef.spaceshipPieces < 5)
            {
                print("1");
                SpaceshipPopUp.Instance.PopUp();
                other.GetComponent<PlayerHealth>().SetHealth(other.GetComponent<PlayerHealth>().GetMaxHealth());
                other.GetComponent<AllPlayerReferences>().HUDref.GetComponent<HUD>().SetVisualHealth();
                _audioSource.Play();
            }

            else
            {
                print("2");
                print("Congratulations !");
                other.gameObject.GetComponent<AllPlayerReferences>().invRef.CancelTimer();
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

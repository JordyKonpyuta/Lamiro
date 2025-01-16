using UnityEngine;
using UnityEngine.Audio;

public class Heal : MonoBehaviour
{
    private CapsuleCollider _playerBodyRef;

    public AudioResource healSound;
    private AudioSource _audioSource;
    
    // -------------------- //
    //       FUNCTIONS      //
    // -------------------- //

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerBodyRef = transform.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Heal
        if (Input.GetButtonDown("Heal"))
        {
            Healing();
        }
    }

    private void Healing()
    {
        if (transform.GetComponent<Inventory>().GetScrews() >= 10 && transform.GetComponent<PlayerHealth>().GetHealth() < transform.GetComponent<PlayerHealth>().GetMaxHealth())
        {
            transform.GetComponent<PlayerHealth>().SetHealth(transform.GetComponent<PlayerHealth>().GetHealth() + 1);
            transform.GetComponent<Inventory>().AddScrews(-10);
            gameObject.GetComponent<AllPlayerReferences>().HUDref.SetVisualHealth();
            gameObject.GetComponent<AllPlayerReferences>().HUDref.UpdateScrewsText(transform.GetComponent<Inventory>().GetScrews());
            _audioSource.resource = healSound;
            _audioSource.Play();
        }
    }
}

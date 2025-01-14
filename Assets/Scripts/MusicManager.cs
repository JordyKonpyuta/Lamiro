using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public AudioResource music;

    // Singleton to be accessible everywhere
    private static MusicManager _instance;

    public static MusicManager Instance
    {
        get
        {
            if (_instance == null)
            {
                print("Music Manager is NULL");
            }

            return _instance;
        }
    }
    
    private void Start()
    {
        PlaySound();
    }

    public void PlaySound()
    {
        gameObject.GetComponent<AudioSource>().resource = music;
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void StopSound()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }
}

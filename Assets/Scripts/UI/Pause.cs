using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    
    public Text spaceshipPiecesText;

    public AudioResource popUpAudio;
    public AudioResource popOutAudio;
    
    // Singleton to be accessible anywhere
    
    private static Pause _instance;

    public static Pause Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Player is null!");
            }

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
        spaceshipPiecesText.text = "Spaceship Pieces : " + Inventory.Instance.spaceshipPieces;
    }

    public void Animation()
    {
        GetComponent<Canvas>().enabled = true;
        GetComponent<Animator>().SetBool("IsOpen", !GetComponent<Animator>().GetBool("IsOpen"));
        MusicManager.Instance.StopSound();
        PlaySound(popUpAudio);
        Time.timeScale = 0;
    }

    public void QuitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        PlaySound(popOutAudio);
        GetComponent<Animator>().SetBool("IsOpen", !GetComponent<Animator>().GetBool("IsOpen"));
        MusicManager.Instance.PlaySound();
        GetComponent<Canvas>().enabled = false;
    }

    public void PlaySound(AudioResource audio)
    {
        gameObject.GetComponent<AudioSource>().resource = audio;
        gameObject.GetComponent<AudioSource>().Play();
    }
    
    
}

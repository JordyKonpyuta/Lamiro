using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public AudioResource popUpSound;

    
    // Singleton to be accessible anywhere
    private static GameOver _instance;

    public static GameOver Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game Over is NULL !");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        GetComponent<Canvas>().enabled = false;
        MusicManager.Instance.StopSound();
        Animation();
        PlaySound(popUpSound);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Animation()
    {
        GetComponent<Animator>().SetBool("IsOpen", !GetComponent<Animator>().GetBool("IsOpen"));
    }

    public void PlaySound(AudioResource audio)
    {
        gameObject.GetComponent<AudioSource>().resource = audio;
        gameObject.GetComponent<AudioSource>().Play();
    }
}

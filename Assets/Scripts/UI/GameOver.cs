using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public AudioResource popUpSound;
    
    private void Start()
    {
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

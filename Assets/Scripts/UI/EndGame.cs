using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public Text scoreText;
    public Image rank;

    public Sprite[] rankImages;

    public AudioResource popUpSound;

    private int score;

    void Start()
    {
        MusicManager.Instance.StopSound();
        CalculateScore();
        PlaySound(popUpSound);
    }

    public void CalculateScore()
    {
        score += Inventory.Instance.GetScrews();
        scoreText.text = "Score : " + score;
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlaySound(AudioResource audio)
    {
        gameObject.GetComponent<AudioSource>().resource = audio;
        gameObject.GetComponent<AudioSource>().Play();
    }

}

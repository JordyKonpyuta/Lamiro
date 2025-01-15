using System;
using Unity.VisualScripting;
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

    private static EndGame _instance;

    public static EndGame Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("End Game is NULL !");
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
    }

    public void OnPopUp()
    {
        GetComponent<Canvas>().enabled = true;
        MusicManager.Instance.StopSound();
        PlaySound(popUpSound);
        GetComponent<Animator>().SetBool("IsOpen", true);
        CalculateScore();
    }

    public void CalculateScore()
    {
        score += Inventory.Instance.GetScrews();
        score += (int)((1800 - Inventory.Instance.timer) * 0.5);
        scoreText.text = "Score : " + score;
        rank.sprite = score switch
        {
            <= 300 => rankImages[4],
            <= 400 => rankImages[3],
            <= 500 => rankImages[2],
            <= 700 => rankImages[1],
            > 700 => rankImages[0]
        };
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

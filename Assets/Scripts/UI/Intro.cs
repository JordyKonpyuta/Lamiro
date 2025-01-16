using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public Image introImage;
    public Sprite[] introImages;

    public Text introText;
    public string[] introTexts;
    
    private AudioSource _audiosource;

    private int _indexDialogue = 0;

    private void Awake()
    {
        _audiosource = GetComponent<AudioSource>();
    }

    public void NextButton()
    {
        if (_indexDialogue < introImages.Length)
        {
            _indexDialogue++;
            DisplayNextDialogue(_indexDialogue);
        }
        else
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    private void DisplayNextDialogue(int index)
    {
        introImage.sprite = introImages[index];
        introText.text = introTexts[index];
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlaySound(AudioResource audio)
    {
        _audiosource.resource = audio;
        _audiosource.Play();
    }
}

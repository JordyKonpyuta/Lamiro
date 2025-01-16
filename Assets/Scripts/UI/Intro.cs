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
        introImage.sprite = introImages[_indexDialogue];
        introText.text = introTexts[_indexDialogue];
    }

    public void NextButton()
    {
        GetComponent<Animator>().SetBool("Next", true);
        Invoke(nameof(Animation), 1);
    }

    private void DisplayNextDialogue()
    {
        if (_indexDialogue < introImages.Length - 1)
        {
            _indexDialogue++;
            introImage.sprite = introImages[_indexDialogue];
            introText.text = introTexts[_indexDialogue];
        }
        else
        {
            SceneManager.LoadScene("GameScene");
        }
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

    private void Animation()
    {
        GetComponent<Animator>().SetBool("Next", false);
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public Text scoreText;
    public Image rank;

    public Sprite[] rankImages;

    private int score;

    void Start()
    {
        CalculateScore();
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

}

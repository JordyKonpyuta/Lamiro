using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    
    public Text spaceshipPiecesText;
    
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
        Animation();
        Time.timeScale = 0;
        spaceshipPiecesText.text = "Spaceship Pieces : " + Inventory.Instance.spaceshipPieces;
    }

    public void Animation()
    {
        GetComponent<Animator>().SetBool("IsOpen", !GetComponent<Animator>().GetBool("IsOpen"));
    }

    public void QuitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        Animation();
    }
    
    
}

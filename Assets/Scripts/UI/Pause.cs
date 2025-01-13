using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    public string mainMenu;
    public Text spaceshipPiecesText;
    
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
        SceneManager.LoadScene(mainMenu);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        Animation();
    }
    
    
}

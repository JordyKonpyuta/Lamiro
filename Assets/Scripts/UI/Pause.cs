using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public string mainMenu;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Animation();
        Time.timeScale = 0;
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

using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    
    public Text screwsText;
    
    // Images for inventory
    public Image jetpackIcon;
    public Image gunIcon;
    public Image swordIcon;
    
    // Player
    private GameObject _player;
    
    // Images for Health
    public Image[] healthImages;
    
    // Singleton to be accessible anywhere
    private static HUD _instance;

    public static HUD Instance
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

    void Start()
    {
        jetpackIcon.enabled = false;
        swordIcon.enabled = false;
        gunIcon.enabled = false;
        _player = GameObject.FindGameObjectWithTag("Player");
        
        SetVisualHealth();
        UpdateScrewsText(_player.GetComponent<Inventory>().GetScrews());
    }

    
    // Called when a screw is picked up
    public void UpdateScrewsText(int screws)
    {
        screwsText.text = "" + screws;
    }

    public void SetVisualHealth()
    {
        foreach (Image image in healthImages)
        {
            image.enabled = false;
        }
        for (int i = 0; i < _player.GetComponent<PlayerHealth>().GetHealth(); i++)
        {
            healthImages[i].enabled = true;
        }
    }
    

}

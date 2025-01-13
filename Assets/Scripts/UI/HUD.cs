using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    
    public Text screwsText;
    
    // Images for inventory
    public Image jetpackIcon;
    public Image gunIcon;
    public Image swordIcon;
    
    
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

    
    // Called when a screw is picked up
    public void UpdateScrewsText(int screws)
    {
        screwsText.text = "" + screws;
    }

    public void SetVisualHealth()
    {
        for (int i = 0; i < PlayerHealth.Instance.GetHealth(); i++)
        {
            healthImages[i].enabled = true;
        }
    }
    

}

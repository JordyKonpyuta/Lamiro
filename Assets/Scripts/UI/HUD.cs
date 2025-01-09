using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    
    public Text screwsText;
    public Image[] spaceshipPiecesImages;

    // Images for the weapon Icon
    public Image[] weaponIcons;
    
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

    // Set Weapon Icon
    public void SetWeaponIcon(int index)
    {
        weaponIcons[index].enabled = true;
    }

}

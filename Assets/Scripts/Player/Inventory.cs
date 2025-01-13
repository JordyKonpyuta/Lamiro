using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int screws;
    
    // Singleton to be accessible anywhere
    
    private static Inventory _instance;
    
    public int spaceshipPieces = 0;

    public static Inventory Instance
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
    
    // Getters
    public int GetScrews()
    {
        return screws;
    }
    
    
    // Setters
    public void AddScrews()
    {
        screws++;
    }
    

}

using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int screws;
    public int spaceshipPieces = 0;
    public float timer = 0;
    
    // Singleton to be accessible anywhere
    
    private static Inventory _instance;
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

    void Update()
    {
            timer += Time.deltaTime;
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

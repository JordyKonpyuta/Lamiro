using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int screws;
    
    // Singleton to be accessible anywhere
    
    private static Player _instance;

    public static Player Instance
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

    
    // Getter and Setter for screws
    public int GetScrews()
    {
        return screws;
    }
    
    public void AddScrews()
    {
        screws++;
    }
}

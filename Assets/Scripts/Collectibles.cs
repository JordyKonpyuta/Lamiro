using UnityEngine;

public class Collectibles : MonoBehaviour
{
    
    // Singleton to be accessible anywhere
    
    private static Collectibles _instance;
    public Enum_Collectibles.CollectibleType type;

    public static Collectibles Instance
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

    
    // When player Collide with the collectible
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // If the collectible is a screw
            if (type == Enum_Collectibles.CollectibleType.Screws)
            {
                Player.Instance.AddScrews();
                HUD.Instance.UpdateScrewsText(Player.Instance.GetScrews());
                Destroy(this);
            }
        }
    }
}

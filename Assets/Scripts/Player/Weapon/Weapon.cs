using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Singleton to be accessible anywhere
    
    private static Weapon _instance;

    public static Weapon Instance
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
    
    public EnumWeapon.WeaponType weaponEquipped;
    
}

using UnityEngine;

public class Collectibles : MonoBehaviour
{

    public Enum_Collectibles.CollectibleType type;

    public int spaceshipPieceIndex = 0;
    
    
    // Singleton to be accessible anywhere
    
    private static Collectibles _instance;

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
            switch (type)
            {
                case Enum_Collectibles.CollectibleType.Screws :
                    Player.Instance.AddScrews();
                    HUD.Instance.UpdateScrewsText(Player.Instance.GetScrews());
                    Destroy(this);
                    break;
                case Enum_Collectibles.CollectibleType.Gun :
                    Weapon.Instance.weaponEquipped = EnumWeapon.WeaponType.Gun;
                    HUD.Instance.SetWeaponIcon(HUD.Instance.gunSprite);
                    Destroy(this);
                    break;
                case Enum_Collectibles.CollectibleType.Sword :
                    Weapon.Instance.weaponEquipped = EnumWeapon.WeaponType.Sword;
                    HUD.Instance.SetWeaponIcon(HUD.Instance.swordSprite);
                    Destroy(this);
                    break;
                case Enum_Collectibles.CollectibleType.Jetpack :
                    break;
                case Enum_Collectibles.CollectibleType.SpaceshipPieces :
                    Player.Instance.ObtainSpaceshipPiece(spaceshipPieceIndex);
                    HUD.Instance.spaceshipPiecesImages[spaceshipPieceIndex].enabled = true;
                    Destroy(this);
                    break;
            }
        }
    }
}

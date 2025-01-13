using UnityEngine;

public class Collectibles : MonoBehaviour
{

    public Enum_Collectibles.CollectibleType type;

    public Transform[] meshes;

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

        switch (type)
        {
            case Enum_Collectibles.CollectibleType.Sword :
                ActivateMesh(0);
                break;
            case Enum_Collectibles.CollectibleType.Screws :
                ActivateMesh(1);
                break;
            case Enum_Collectibles.CollectibleType.Gun :
                ActivateMesh(2);
                break;
            case Enum_Collectibles.CollectibleType.Jetpack :
                ActivateMesh(3);
                break;
        }
    }

    
    // When player Collide with the collectible
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case Enum_Collectibles.CollectibleType.Screws :
                    other.gameObject.GetComponent<Inventory>().AddScrews();
                    HUD.Instance.UpdateScrewsText(Inventory.Instance.GetScrews());
                    Destroy(gameObject);
                    break;
                case Enum_Collectibles.CollectibleType.Gun :
                    Weapon.Instance.weaponEquipped = EnumWeapon.WeaponType.Gun;
                    HUD.Instance.gunIcon.enabled = true;
                    Destroy(gameObject);
                    break;
                case Enum_Collectibles.CollectibleType.Sword :
                    Weapon.Instance.weaponEquipped = EnumWeapon.WeaponType.Sword;
                    HUD.Instance.swordIcon.enabled = true;
                    Destroy(gameObject);
                    break;
                case Enum_Collectibles.CollectibleType.Jetpack :
                    HUD.Instance.jetpackIcon.enabled = true;
                    Destroy(gameObject);
                    break;
                case Enum_Collectibles.CollectibleType.SpaceshipPieces :
                    other.gameObject.GetComponent<Inventory>().ObtainSpaceshipPiece(spaceshipPieceIndex);
                    Destroy(gameObject);
                    break;
            }
        }
    }

    private void ActivateMesh(int index)
    {
        for (int i = 0; i < meshes.Length; i++)
        {
            if (index == i)
                meshes[i].gameObject.SetActive(true);
            else 
                meshes[i].gameObject.SetActive(false);
        }
    }
}

using UnityEngine;

public class Enum_Collectibles : MonoBehaviour
{
    public enum CollectibleType
    {
        Screws,
        SpaceshipPieces,
        Sword,
        Gun,
        Jetpack
    }

    public CollectibleType type;
}

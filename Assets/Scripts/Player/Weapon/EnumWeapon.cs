using UnityEngine;

public class EnumWeapon : MonoBehaviour
{
    public enum WeaponType
    {
        None,
        Sword,
        Gun
    }

    public WeaponType type;
}

using UnityEngine;
using UnityEngine.Serialization;

public class LinkedObject : MonoBehaviour
{
    public Enum_LinkedObjectsType.LinkedObjectType objectType;
    
    public void Interaction()
    {
        print("Interaction");
    }
}
